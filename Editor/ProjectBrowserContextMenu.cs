using System;
using System.Collections.Generic;
using System.Reflection;
using SaintsProject.Editor.UIElement;
using UnityEditor;
using UnityEngine;

namespace SaintsProject.Editor
{
    // Menu data belongs to one invocation. Only the startup choice and hook ownership survive it.
    internal sealed class ProjectBrowserContextMenu
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
        internal static bool Enabled { get; private set; }
        private static bool _sampled;
        private readonly EditorWindow _window;
        private ProjectPopupMenu _dropdown;

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            // Wait until ScriptableSingleton deserialization has completed. Do not subscribe to config changes.
            EditorApplication.delayCall += SampleConfiguration;
        }

        internal static void SampleConfiguration()
        {
            if (_sampled) return;
            Enabled = !ProjectConfigStore.Active.Disabled && ProjectConfigStore.Active.ReplaceContextMenu;
            _sampled = true;
        }

        internal sealed class Entry
        {
            internal string Path;
            internal string Label;
            internal string Shortcut;
            internal int Priority;
            internal bool Separator;
            internal bool Enabled;
            internal bool Checked;
            internal Action Execute;
            internal readonly List<Entry> Children = new List<Entry>();

            public Entry NewChildren(IEnumerable<Entry> children)
            {
                Entry result = new Entry
                {
                    Path = Path,
                    Label = Label,
                    Shortcut = Shortcut,
                    Priority = Priority,
                    Separator = false,
                    Enabled = Enabled,
                    Checked = Checked,
                    Execute = Execute,
                };
                result.Children.AddRange(children);
                return result;
            }
        }

        internal ProjectBrowserContextMenu(EditorWindow window) => _window = window;

        internal void Dispose()
        {
            _dropdown?.Close();
            _dropdown = null;
        }

        internal void Draw(Action original)
        {
            Event evt = Event.current;
            if (!Enabled || evt == null || evt.type != EventType.ContextClick || _window.rootVisualElement.panel == null)
            {
                original();
                return;
            }

            // MouseDown has already selected the clicked item/active folder through Unity's code.
            bool listVisible = Convert.ToInt32(
                                  Get(_window, "m_ViewMode")) == 1
                                        || (bool)(Call(
                                                Get(_window, "m_SearchFilter"),
                                                "IsSearching"
                                            )
                                        ?? false);
            if (listVisible
                && Get(_window, "m_ListAreaRect") is Rect listRect
                && listRect.Contains(evt.mousePosition))
            {
                if (TryShowAssets(false))
                {
                    GUIUtility.hotControl = 0;
                    evt.Use();
                }
                original();
                return;
            }

            // Install only for this draw, after Unity's own tree selection handling and before its popup callback.
            List<Action> restore = new List<Action>();
            try
            {
                HookTree(Get(_window, "m_AssetTree"), false, restore);
                HookTree(Get(_window, "m_FolderTree"), true, restore);
            }
            catch
            {
                for (int i = restore.Count - 1; i >= 0; i--) restore[i]();
                throw;
            }

            try
            {
                original();
            }
            finally
            {
                for (int i = restore.Count - 1; i >= 0; i--)
                {
                    restore[i]();
                }
            }
        }

        private void HookTree(object tree, bool folders, List<Action> restore)
        {
            if (tree == null)
            {
                return;
            }
            PropertyInfo callback = tree.GetType().GetProperty("contextClickItemCallback", Flags);
            if (callback?.GetValue(tree) is Delegate previous
                && previous.GetInvocationList().Length == 1
                && previous.Method.DeclaringType == _window.GetType())
            {
                Type idType = previous.GetType().GetGenericArguments()[0];
                MethodInfo factory = GetType().GetMethod(nameof(ItemCallback), Flags)!.MakeGenericMethod(idType);
                Delegate installed = (Delegate)factory.Invoke(this, new[] { previous, folders, tree });
                callback.SetValue(tree, installed);
                restore.Add(() =>
                {
                    if (Equals(callback.GetValue(tree), installed))
                    {
                        callback.SetValue(tree, previous);
                    }
                });
            }

            PropertyInfo outside = tree.GetType().GetProperty("contextClickOutsideItemsCallback", Flags);
            if (!folders
                && outside?.GetValue(tree) is Action oldOutside
                && oldOutside.GetInvocationList().Length == 1
                && oldOutside.Method.DeclaringType == _window.GetType())
            {
                Action installed = () =>
                    OutsideItems(
                        tree,
                        oldOutside,
                        () => TryShowAssets(false)
                    );
                outside.SetValue(tree, installed);
                restore.Add(() =>
                {
                    if (Equals(outside.GetValue(tree), installed))
                    {
                        outside.SetValue(tree, oldOutside);
                    }
                });
            }
        }

        internal void OutsideItems(object tree, Action native, Func<bool> show)
        {
            bool handled = TryReplace(() =>
            {
                // Unity clears the one-column tree selection before validating its blank-area menu.
                Array treeSelection = (Array)Call(tree, "GetSelection");
                if (treeSelection == null)
                {
                    throw new MissingMemberException("Tree selection");
                }

                if (treeSelection.Length == 0)
                {
                    return show();
                }

                treeSelection = (Array)treeSelection.Clone();
                PropertyInfo selection = typeof(Selection).GetProperty("entityIds", Flags)
                                         ?? typeof(Selection).GetProperty("instanceIDs", Flags);
                PropertyInfo active = typeof(Selection).GetProperty("activeEntityId", Flags)
                                      ?? typeof(Selection).GetProperty("activeInstanceID", Flags);
                Array globalSelection = (Array)(selection?.GetValue(null) as Array)?.Clone();
                object activeId = active?.GetValue(null);
                MethodInfo restoreGlobal = typeof(Selection).GetMethod(
                    selection?.Name == "entityIds" ? "SetSelectionWithActiveEntityId" : "SetSelectionWithActiveInstanceID",
                    Flags);
                MethodInfo restoreTree = tree.GetType().GetMethod(
                    "SetSelection",
                    Flags,
                    null,
                    new[] { treeSelection.GetType(), typeof(bool) },
                    null
                );
                if (globalSelection == null || activeId == null || restoreGlobal == null || restoreTree == null)
                {
                    throw new MissingMemberException("Selection restoration");
                }
                bool ready = false;
                try
                {
                    ClearTreeSelection(tree);
                    ready = show();
                    return ready;
                }
                finally
                {
                    if (!ready)
                    {
                        restoreTree.Invoke(tree, new object[] { treeSelection, false });
                        restoreGlobal.Invoke(null, new[] { globalSelection, activeId });
                    }
                }
            });
            if (handled)
            {
                Event.current.Use();
            }
            else
            {
                native();
            }
        }

        private Action<T> ItemCallback<T>(Delegate previous, bool folders, object tree)
        {
            Action<T> native = (Action<T>)previous;
            return id =>
            {
                if (!folders && EqualityComparer<T>.Default.Equals(id, default))
                {
                    // Nonselectable assets intentionally have no menu in Unity's normal tree.
                    if (TryReplace(() =>
                        {
                            ClearTreeSelection(tree);
                            return true;
                        }))
                    {
                        Event.current.Use();
                    }
                    else
                    {
                        native(id);
                    }

                    return;
                }
                bool handled = TryReplace(() =>
                {
                    if (folders
                        && string.Equals(
                            Call(_window.GetType(), "GetItemType", id)?.ToString(),
                            "SavedFilter",
                            StringComparison.Ordinal
                        )
                    )
                    {
                        return TryShowSavedFilter(id);
                    }
                    return TryShowAssets(folders);
                });
                if (handled)
                {
                    Event.current.Use();
                }
                else
                {
                    native(id);
                }
            };
        }

        private void ClearTreeSelection(object tree)
        {
            Array selected = (Array)Call(tree, "GetSelection");
            if (selected == null)
            {


            }
            if (selected.Length == 0)
            {
                return;
            }

            Array empty = Array.CreateInstance(selected.GetType().GetElementType(), 0);
            MethodInfo setSelection = tree.GetType().GetMethod("SetSelection", Flags, null, new[] { empty.GetType(), typeof(bool) }, null);
            MethodInfo notify = _window.GetType().GetMethod("AssetTreeSelectionCallback", Flags);
            if (setSelection == null || notify == null)
            {
                throw new MissingMemberException("Tree selection callbacks");
            }
            setSelection.Invoke(tree, new object[] { empty, false });
            notify.Invoke(_window, new object[] { empty });
        }

        private bool TryShowAssets(bool folders)
        {
            return TryReplace(() =>
            {
                // A mode can replace the native Assets menu independently of its registered commands.
                Type mode = typeof(EditorWindow).Assembly.GetType("UnityEditor.ModeService");
                if ((bool)(Call(mode, "HasContextMenu", "Assets/") ?? false)) return false;
                PropertyInfo safeMode = typeof(EditorUtility).GetProperty("isInSafeMode", Flags);
                if ((bool)(safeMode?.GetValue(null) ?? false) && Selection.objects.Length == 0) return false;
                List<Entry> entries = ReadAssets(_window, folders);
                return entries.Count > 0 && Show(entries);
            });
        }

        internal bool TryReplace(Func<bool> prepare)
        {
            try
            {
                return prepare();
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        internal static List<Entry> ReadAssets(EditorWindow window, bool folders)
        {
            MethodInfo getItems = typeof(Menu).GetMethod("GetMenuItems", Flags);
            if (getItems == null) throw new MissingMethodException("Menu.GetMenuItems");
            Array items = (Array)getItems.Invoke(null, new object[] { "Assets", true, false });
            Array localized = (Array)getItems.Invoke(null, new object[] { "Assets", true, true });
            if (items == null || localized == null || items.Length != localized.Length) throw new InvalidOperationException("Menu metadata changed during enumeration.");

            PropertyInfo selectionProperty = typeof(Selection).GetProperty("entityIds", Flags) ?? typeof(Selection).GetProperty("instanceIDs", Flags);
            Array selection = (Array)selectionProperty?.GetValue(null);
            if (selection == null) throw new MissingMemberException("Selection IDs");
            selection = (Array)selection.Clone();
            Entry root = new Entry { Path = "Assets" };
            Dictionary<string, Entry> branches = new Dictionary<string, Entry> { ["Assets"] = root };
            MethodInfo hotkey = typeof(Menu).GetMethod("GetHotkey", Flags);
            for (int i = 0; i < items.Length; i++)
            {
                object item = items.GetValue(i);
                string path = (string)Get(item, "path");
                bool separator = (bool)Get(item, "isSeparator");
                int priority = (int)Get(item, "priority");
                if (separator && branches.TryGetValue(path, out Entry branch))
                {
                    branch.Children.Add(new Entry { Separator = true });
                    continue;
                }
                if (!path.StartsWith("Assets/", StringComparison.Ordinal)) continue;
                string displayPath = (string)Get(localized.GetValue(i), "path");
                Entry parent = EnsureBranch(path.Substring(0, path.LastIndexOf('/')), branches);
                Entry entry = new Entry
                {
                    Path = path,
                    Label = displayPath.Substring(displayPath.LastIndexOf('/') + 1),
                    Priority = priority,
                    Enabled = IsEnabled(window, path, folders),
                    Checked = Menu.GetChecked(path),
                    Shortcut = (string)hotkey?.Invoke(null, new object[] { path }),
                };
                parent.Children.Add(entry);
                if (separator) branches[path] = entry;
                else
                {
                    string command = path;
                    entry.Execute = () =>
                    {
                        if (!window || !SameSelection(selection, selectionProperty.GetValue(null) as Array)) return;
                        window.Focus();
                        Call(window, "SetAsLastInteractedProjectBrowser");
                        bool wasFolderContext = (bool)(Get(window, "isFolderTreeViewContextClicked") ?? false);
                        try
                        {
                            Set(window, "isFolderTreeViewContextClicked", folders);
                            if (Menu.GetEnabled(command)) EditorApplication.ExecuteMenuItem(command);
                        }
                        finally { Set(window, "isFolderTreeViewContextClicked", wasFolderContext); }
                    };
                }
            }
            AddPrioritySeparators(root);
            return root.Children;
        }

        private static bool IsEnabled(EditorWindow window, string path, bool folders)
        {
            bool previous = (bool)(Get(window, "isFolderTreeViewContextClicked") ?? false);
            try { Set(window, "isFolderTreeViewContextClicked", folders); return Menu.GetEnabled(path); }
            finally { Set(window, "isFolderTreeViewContextClicked", previous); }
        }

        private static bool SameSelection(Array expected, Array actual)
        {
            if (actual == null || actual.Length != expected.Length) return false;
            for (int i = 0; i < expected.Length; i++) if (!Equals(expected.GetValue(i), actual.GetValue(i))) return false;
            return true;
        }

        private static Entry EnsureBranch(string path, Dictionary<string, Entry> branches)
        {
            if (branches.TryGetValue(path, out Entry found)) return found;
            Entry parent = EnsureBranch(path.Substring(0, path.LastIndexOf('/')), branches);
            Entry entry = new Entry { Path = path, Label = path.Substring(path.LastIndexOf('/') + 1), Enabled = true };
            parent.Children.Add(entry);
            branches[path] = entry;
            return entry;
        }

        private static void AddPrioritySeparators(Entry parent)
        {
            Entry previous = null;
            for (int i = 0; i < parent.Children.Count; i++)
            {
                Entry child = parent.Children[i];
                if (child.Separator) { previous = null; continue; }
                // Unity MenuItem's documented divider rule: adjacent priorities differ by at least 11.
                if (previous != null && child.Priority - previous.Priority > 10)
                {
                    parent.Children.Insert(i++, new Entry { Separator = true });
                }
                AddPrioritySeparators(child);
                previous = child;
            }
        }

        private bool TryShowSavedFilter(object id)
        {
            List<Entry> entries = ReadSavedFilter(_window.GetType(), id);
            return entries.Count == 0 || Show(entries);
        }

        internal static List<Entry> ReadSavedFilter(Type browser, object id)
        {
            Type filters = typeof(EditorWindow).Assembly.GetType("UnityEditor.SavedSearchFilters", true);
            Type menu = browser.GetNestedType("SavedFiltersContextMenu", Flags)
                        ?? throw new MissingMemberException(browser.FullName, "SavedFiltersContextMenu");
            object filterId = id;
            MethodInfo rootId = filters.GetMethod("GetRootFilterId", Flags) ?? filters.GetMethod("GetRootInstanceID", Flags)
                                ?? throw new MissingMethodException(filters.FullName, "GetRootFilterId / GetRootInstanceID");
            Type mapping = typeof(EditorWindow).Assembly.GetType("UnityEditor.FavoritesEntityIds");
            if (mapping != null) filterId = Call(Get(mapping, "instance"), "GetIdFor", id)
                                          ?? throw new MissingMemberException("FavoritesEntityIds.GetIdFor");
            if (Equals(filterId, rootId.Invoke(null, null)))
            {
                return new List<Entry>();
            }
            object context = Activator.CreateInstance(menu, Flags, null, new[] { filterId }, null);
            MethodInfo delete = menu.GetMethod("Delete", Flags)
                                ?? throw new MissingMethodException(menu.FullName, "Delete");
            return new List<Entry> { new Entry { Label = L10n.Tr("Delete"), Enabled = true, Execute = () => delete.Invoke(context, null) } };
        }

        private bool Show(List<Entry> entries)
        {
            if (_window.rootVisualElement.panel == null) return false;
            Vector2 position = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            _dropdown?.Close();
            _dropdown = new ProjectPopupMenu(_window, entries, position);
            _dropdown.Show();
            return true;
        }

        internal static Vector2 PanelPoint(Vector2 guiPoint)
        {
            // Keep everything in GUI points, including the current scroll/group offsets and editor DPI.
            Type clip = typeof(GUI).Assembly.GetType("UnityEngine.GUIClip");
            MethodInfo unclip = clip?.GetMethod("UnclipToWindow", Flags, null, new[] { typeof(Vector2) }, null);
            if (unclip == null)
            {
                throw new MissingMethodException("GUIClip.UnclipToWindow");
            }
            return (Vector2)unclip.Invoke(null, new object[] { guiPoint });
        }

        private static object Get(object target, string name)
        {
            if (target == null)
            {
                return null;
            }
            Type type = target as Type ?? target.GetType();
            object instance = target is Type ? null : target;
            return type.GetField(name, Flags)?.GetValue(instance) ?? type.GetProperty(name, Flags)?.GetValue(instance);
        }

        private static void Set(object target, string name, object value) => target.GetType().GetField(name, Flags)?.SetValue(target, value);

        private static object Call(object target, string name, params object[] arguments)
        {
            if (target == null)
            {
                return null;
            }
            Type type = target as Type ?? target.GetType();
            foreach (MethodInfo method in type.GetMethods(Flags))
            {
                if (method.Name != name || method.GetParameters().Length != arguments.Length) continue;
                ParameterInfo[] parameters = method.GetParameters();
                bool matches = true;
                for (int i = 0; i < arguments.Length; i++)
                    if (arguments[i] != null && !parameters[i].ParameterType.IsInstanceOfType(arguments[i])) matches = false;
                if (matches) return method.Invoke(target is Type ? null : target, arguments);
            }
            return null;
        }
    }
}
