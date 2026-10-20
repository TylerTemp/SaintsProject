using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SaintsProject.Editor.UIElement
{
    // Unity supplies each menu's rows and styling; this adapter only coordinates cascading menus.
    internal sealed class ProjectContextDropdown
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        internal static bool Supported => Supports(typeof(GenericDropdownMenu));
        internal static bool Supports(Type type) => type.GetField("m_MenuContainer", Flags)?.FieldType == typeof(VisualElement)
            && type.GetField("m_ScrollView", Flags)?.FieldType == typeof(ScrollView)
            && type.GetMethod("OnFocusOut", Flags) != null
            && type.GetMethod("Hide", Flags) != null
            && type.GetMethod("GetSelectedIndex", Flags)?.ReturnType == typeof(int)
            && type.GetProperty("isSingleSelectionDropdown", Flags)?.CanWrite == true;

        private sealed class Level
        {
            internal GenericDropdownMenu Menu;
            internal VisualElement Container;
            internal VisualElement Outer;
            internal readonly List<VisualElement> Rows = new List<VisualElement>();
            internal readonly List<ProjectBrowserContextMenu.Entry> Entries = new List<ProjectBrowserContextMenu.Entry>();
            internal ProjectBrowserContextMenu.Entry Parent;
        }

        private readonly EditorWindow _window;
        private readonly VisualElement _root;
        private readonly List<ProjectBrowserContextMenu.Entry> _entries;
        private readonly Vector2 _position;
        private readonly List<Level> _levels = new List<Level>();
        private bool _closed;

        internal ProjectContextDropdown(EditorWindow window, List<ProjectBrowserContextMenu.Entry> entries, Vector2 panelPosition)
        {
            _window = window;
            _root = window.rootVisualElement;
            _entries = entries;
            _position = panelPosition;
        }

        internal void Show()
        {
            _root.RegisterCallback<PointerDownEvent>(OnPointerDown, TrickleDown.TrickleDown);
            _root.RegisterCallback<FocusOutEvent>(OnFocusOut);
            _root.RegisterCallback<DetachFromPanelEvent>(OnDetach);
            EditorApplication.update += CheckWindow;
            try { OpenLevel(_entries, _position, null); }
            catch { Close(); throw; }
        }

        internal void Close()
        {
            if (_closed) return;
            _closed = true;
            CloseFrom(0);
            _root.UnregisterCallback<PointerDownEvent>(OnPointerDown, TrickleDown.TrickleDown);
            _root.UnregisterCallback<FocusOutEvent>(OnFocusOut);
            _root.UnregisterCallback<DetachFromPanelEvent>(OnDetach);
            EditorApplication.update -= CheckWindow;
        }

        private void CheckWindow()
        {
            if (!_window || !_window.hasFocus || _root.panel == null || _levels.Count == 0 || _levels[0].Container.panel == null) Close();
        }

        private void OnDetach(DetachFromPanelEvent evt)
        {
            if (evt.target == _root) Close();
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            foreach (Level level in _levels)
                if (level.Outer.worldBound.Contains(evt.position)) return;
            Close();
        }

        private void OnFocusOut(FocusOutEvent evt)
        {
            _root.schedule.Execute(() =>
            {
                if (_closed) return;
                VisualElement focused = _root.focusController?.focusedElement as VisualElement;
                foreach (Level level in _levels)
                    if (focused != null && level.Container.Contains(focused)) return;
                Close();
            });
        }

        private void CloseFrom(int depth)
        {
            while (_levels.Count > depth)
            {
                Level level = _levels[_levels.Count - 1];
                _levels.RemoveAt(_levels.Count - 1);
                typeof(GenericDropdownMenu).GetMethod("Hide", Flags).Invoke(level.Menu, new object[] { false });
            }
        }

        private void OpenLevel(List<ProjectBrowserContextMenu.Entry> entries, Vector2 position, ProjectBrowserContextMenu.Entry parent)
        {
            Level level = new Level { Menu = new GenericDropdownMenu(), Parent = parent };
            int depth = _levels.Count;
            foreach (ProjectBrowserContextMenu.Entry entry in entries)
            {
                if (entry.Separator)
                {
                    level.Menu.AddSeparator("");
                    continue;
                }
                int previousCount = level.Menu.contentContainer.childCount;
                if (!entry.Enabled) level.Menu.AddDisabledItem(entry.Label, entry.Checked);
                else level.Menu.AddItem(entry.Label, entry.Checked, () => Activate(level, entry));
                if (level.Menu.contentContainer.childCount == previousCount) continue;
                VisualElement row = level.Menu.contentContainer[level.Menu.contentContainer.childCount - 1];
                level.Rows.Add(row);
                level.Entries.Add(entry);
                // Shortcut and submenu markers occupy the native menu's right-hand column.
                if (entry.Children.Count > 0 || !string.IsNullOrEmpty(entry.Shortcut))
                {
                    Label suffix = new Label(entry.Children.Count > 0 ? "›" : entry.Shortcut) { pickingMode = PickingMode.Ignore };
                    suffix.style.marginLeft = 24;
                    suffix.style.marginRight = 6;
                    suffix.style.unityTextAlign = TextAnchor.MiddleRight;
                    suffix.style.flexShrink = 0;
                    VisualElement content = row[0];
                    content.style.flexDirection = FlexDirection.Row;
                    content.Q<Label>().style.flexGrow = 1;
                    content.Add(suffix);
                }
                row.RegisterCallback<PointerEnterEvent>(_ =>
                {
                    if (!entry.Enabled) return;
                    row.schedule.Execute(() =>
                    {
                        if (_closed || row.panel == null || depth >= _levels.Count || _levels[depth] != level) return;
                        // Cancel an old hover request when the pointer has moved to another row.
                        int selected = (int)typeof(GenericDropdownMenu).GetMethod("GetSelectedIndex", Flags).Invoke(level.Menu, null);
                        if (selected < 0 || selected >= level.Rows.Count || level.Rows[selected] != row) return;
                        if (entry.Children.Count > 0) OpenChild(level, entry, row);
                        else CloseFrom(depth + 1);
                    }).StartingIn(150);
                });
            }

            level.Container = (VisualElement)typeof(GenericDropdownMenu).GetField("m_MenuContainer", Flags).GetValue(level.Menu);
            ScrollView scroll = (ScrollView)typeof(GenericDropdownMenu).GetField("m_ScrollView", Flags).GetValue(level.Menu);
            level.Outer = scroll.parent;
            typeof(GenericDropdownMenu).GetProperty("isSingleSelectionDropdown", Flags)?.SetValue(level.Menu, false);
            level.Container.pickingMode = PickingMode.Ignore;
            level.Container.RegisterCallback<KeyDownEvent>(evt => OnKeyDown(evt, level), TrickleDown.TrickleDown);
            _levels.Add(level);
#pragma warning disable CS0618 // This overload also exists on Unity 2022.2.
            level.Menu.DropDown(new Rect(position, Vector2.zero), _root, false);
#pragma warning restore CS0618
            // Per-menu focus dismissal would close the parent when a child receives focus.
            var focusOut = (EventCallback<FocusOutEvent>)Delegate.CreateDelegate(typeof(EventCallback<FocusOutEvent>), level.Menu, typeof(GenericDropdownMenu).GetMethod("OnFocusOut", Flags));
            scroll.UnregisterCallback(focusOut);
        }

        private void Activate(Level level, ProjectBrowserContextMenu.Entry entry)
        {
            if (_closed || !entry.Enabled) return;
            int index = level.Entries.IndexOf(entry);
            if (entry.Children.Count > 0) OpenChild(level, entry, level.Rows[index]);
            else
            {
                Close();
                entry.Execute?.Invoke();
            }
        }

        private void OpenChild(Level level, ProjectBrowserContextMenu.Entry entry, VisualElement row)
        {
            int depth = _levels.IndexOf(level) + 1;
            if (depth < _levels.Count && _levels[depth].Parent == entry) return;
            CloseFrom(depth);
            OpenLevel(entry.Children, new Vector2(level.Outer.worldBound.xMax, row.worldBound.yMin), entry);
        }

        private void OnKeyDown(KeyDownEvent evt, Level level)
        {
            if (evt.keyCode == KeyCode.Escape)
            {
                Close();
            }
            else if (evt.keyCode == KeyCode.LeftArrow)
            {
                int depth = _levels.IndexOf(level);
                if (depth > 0)
                {
                    CloseFrom(depth);
                    _levels[depth - 1].Menu.contentContainer.Focus();
                }
            }
            else if (evt.keyCode == KeyCode.RightArrow || evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
            {
                int selected = (int)typeof(GenericDropdownMenu).GetMethod("GetSelectedIndex", Flags).Invoke(level.Menu, null);
                if (selected >= 0 && selected < level.Entries.Count)
                {
                    ProjectBrowserContextMenu.Entry entry = level.Entries[selected];
                    if (evt.keyCode != KeyCode.RightArrow || entry.Children.Count > 0) Activate(level, entry);
                }
            }
            else return;
            evt.StopImmediatePropagation();
            evt.PreventDefault();
        }
    }
}
