using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SaintsProject.Editor.Config;
using SaintsProject.Editor.Utils;
using UnityEditor;
using UnityEngine;
#if UNITY_6000_4_OR_NEWER
using ObjectId = UnityEngine.EntityId;

#else
using ObjectId = System.Int32;

#endif
namespace SaintsProject.Editor
{
    [InitializeOnLoad]
    public static class ProjectEntrance
    {
        private static readonly HashSet<(ObjectId, Rect)> Drawn = new HashSet<(ObjectId, Rect)>();
        private static readonly Dictionary<(Type, string), MemberInfo> Members = new Dictionary<(Type, string), MemberInfo>();
        static ProjectEntrance()
        {
#if UNITY_6000_4_OR_NEWER
            EditorApplication.projectWindowItemByEntityIdOnGUI -= OnItemGUI;
            EditorApplication.projectWindowItemByEntityIdOnGUI += OnItemGUI;
#elif UNITY_2022_1_OR_NEWER
            EditorApplication.projectWindowItemInstanceOnGUI -= OnItemGUI;
            EditorApplication.projectWindowItemInstanceOnGUI += OnItemGUI;
#else
            EditorApplication.projectWindowItemOnGUI -= OnGuidGUI;
            EditorApplication.projectWindowItemOnGUI += OnGuidGUI;
#endif
        }

        // Unity 6000.6 can dispatch the same item twice during one browser draw.
        internal static void BeginGUI() => Drawn.Clear();
#if !UNITY_2022_1_OR_NEWER
        private static void OnGuidGUI(string guid, Rect rect)
        {
            UnityEngine.Object asset = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid));
            if (asset)
            {
                OnItemGUI(asset.GetInstanceID(), rect);
            }
        }

#endif
        private static void OnItemGUI(ObjectId id, Rect rect)
        {
            IConfig config = ProjectConfigStore.Active;
            if (config.Disabled || Event.current == null)
            {
                return;
            }

            if (ProjectWindow.Current && !Drawn.Add((id, rect)))
            {
                return;
            }

#pragma warning disable CS0618 // Unity 6000.3 still delivers int IDs through this callback.

            string path = AssetDatabase.GetAssetPath(id);
#pragma warning restore CS0618
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            Event evt = Event.current;
            if (evt.type == EventType.MouseDown && evt.button == 0 && evt.alt && rect.Contains(evt.mousePosition))
            {
                PopupWindow.Show(rect, new AssetConfigPopup(AssetDatabase.AssetPathToGUID(path)));
                evt.Use();
                return;
            }

            if (evt.type != EventType.Repaint)
            {
                return;
            }

            AssetConfig appearance = ProjectConfigStore.Resolve(path);
            bool grid = rect.height > 20;
            bool folder = AssetDatabase.IsValidFolder(path);
            EditorWindow window = ProjectWindow.Current;
            bool twoColumns = window && Convert.ToInt32(Member(window, "m_ViewMode") ?? 0) == 1;
            bool list = twoColumns && (grid || !folder || Mathf.Approximately(rect.x, 14));
#pragma warning disable CS0618
            bool selected = Selection.Contains(id);
#pragma warning restore CS0618
            if (twoColumns && !list)
            {
                object tree = Member(window, "m_FolderTree");
                object state = Member(tree, "state");
                if (Member(state, "selectedIDs")is IList selectedIds)
                {
                    selected = selectedIds.Contains(id);
                }
            }

            // Leave Unity's rename field and its icon untouched.
            if (selected && EditorGUIUtility.editingTextField)
            {
                return;
            }

            FolderContents.Summary contents = folder && (config.AutoIcons || config.ContentMinimap) ? FolderContents.Get(path) : null;
            string iconName = appearance.icon;
            if (string.IsNullOrEmpty(iconName) && config.AutoIcons && contents != null)
            {
                iconName = contents.AutoIcon;
            }

            Texture corner = Util.GetIcon(iconName);
            bool minimal = folder && !list && !grid && config.MinimalMode && corner;
            bool focused = window ? window.hasFocus : EditorWindow.focusedWindow && EditorWindow.focusedWindow.GetType().Name == "ProjectBrowser";
            float gray = EditorGUIUtility.isProSkin ? (list || grid ? .2f : .2196f) : .76f;
            Color background = new Color(gray, gray, gray);
            if (selected && !grid)
            {
                float inactive = EditorGUIUtility.isProSkin ? .3f : .68f;
                background = focused ? new Color(.1725f, .3647f, .5294f) : new Color(inactive, inactive, inactive);
            }
            else if (!grid && rect.Contains(evt.mousePosition))
            {
                background = Color.Lerp(background, Color.white, EditorGUIUtility.isProSkin ? .04f : .1f);
            }

            Rect iconRect = grid ? new Rect(rect.x, rect.y, rect.width, Mathf.Min(rect.width, rect.height - 14)) : new Rect(rect.x + (list ? 3 : 0), rect.y + (rect.height - 16) / 2, 16, 16);
            if (folder)
            {
                if (minimal || appearance.hasColor)
                {
                    EditorGUI.DrawRect(iconRect, background);
                    Texture main = minimal ? corner : Util.GetIcon("Folder On Icon") ?? AssetDatabase.GetCachedIcon(path);
                    DrawIcon(iconRect, main, appearance.hasColor ? appearance.color : Color.white);
                }

                // In the right pane the folder remains the main icon, even in Minimal Mode.
                if (corner && !minimal)
                {
                    DrawIcon(CornerRect(iconRect), corner, Color.white);
                }
            }
            else if (corner)
            {
                EditorGUI.DrawRect(iconRect, background);
                DrawIcon(iconRect, corner, Color.white);
            }

            if (!grid)
            {
                if (appearance.hasColor && !selected)
                {
                    Color tint = appearance.color;
                    tint.a *= .12f;
                    EditorGUI.DrawRect(rect, tint);
                    EditorGUI.DrawRect(new Rect(rect.xMax - 3, rect.y, 3, rect.height), appearance.color);
                }

                if (config.ContentMinimap && contents != null)
                {
                    DrawMinimap(rect, iconRect.xMax + 2, Path.GetFileName(path), contents);
                }

                if (config.BackgroundStrip && ((int)(rect.y / 16) & 1) == 0)
                {
                    // Overlay the entire row, including the foldout gutter; never erase native controls.
                    EditorGUI.DrawRect(FullRowRect(rect), EditorGUIUtility.isProSkin ? new Color(1, 1, 1, .033f) : new Color(0, 0, 0, .05f));
                }
            }

            if (config.IndentGuides && !grid && !list)
            {
                DrawIndentGuides(rect, id, window, twoColumns);
            }
        }

        // Indentation step Unity's Project tree uses per depth level, and the x where depth 0 begins.
        private const float IndentStep = 14f;
        private const float IndentBase = 16f;
        private static readonly Dictionary<(Type, string), MethodInfo> Methods = new Dictionary<(Type, string), MethodInfo>();

        private static void DrawIndentGuides(Rect rect, ObjectId id, EditorWindow window, bool twoColumns)
        {
            int depth = Mathf.RoundToInt((rect.x - IndentBase) / IndentStep);
            if (depth <= 0 || window == null)
            {
                return;
            }

            object tree = Member(window, twoColumns ? "m_FolderTree" : "m_AssetTree");
            object node = InvokeMethod(tree, "FindItem", id);
            if (node == null)
            {
                return;
            }

            for (int level = 0; level < depth; level++)
            {
                object parent = Member(node, "parent");
                if (parent == null)
                {
                    break;
                }

                float x = rect.x - IndentStep / 2f - level * IndentStep - 0.5f;
                Color color = GuideColor(parent);
                bool hasNext = HasNextSibling(node);

                if (level == 0)
                {
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (hasNext)
                    {
                        EditorGUI.DrawRect(new Rect(x, rect.y, 1, rect.height), color);
                    }
                    else
                    {
                        EditorGUI.DrawRect(new Rect(x, rect.y, 1, rect.height / 2f), color);
                    }

                    EditorGUI.DrawRect(new Rect(x, rect.y + rect.height / 2f - 0.5f, IndentStep / 2f + 1f, 1), color);
                }
                else if (hasNext)
                {
                    EditorGUI.DrawRect(new Rect(x, rect.y, 1, rect.height), color);
                }

                node = parent;
            }
        }

        private static bool HasNextSibling(object node)
        {
            object parent = Member(node, "parent");
            if (parent == null)
            {
                return false;
            }

            if (Member(parent, "children") is not IList children)
            {
                return false;
            }

            int index = children.IndexOf(node);
            return index >= 0 && index < children.Count - 1;
        }

        private static readonly Color DefaultGuideColor = new Color(0.4f, 0.4f, 0.4f);
        private static Color GuideColor(object treeItem)
        {
            object idObj = Member(treeItem, "id");
            if (idObj == null)
            {
                return DefaultGuideColor;
            }

            string path = AssetDatabase.GetAssetPath((ObjectId)idObj);
            if (string.IsNullOrEmpty(path))
            {
                return DefaultGuideColor;
            }

            AssetConfig appearance = ProjectConfigStore.Resolve(path);
            return appearance.hasColor ? appearance.color : DefaultGuideColor;
        }

        private static object InvokeMethod(object target, string name, object arg)
        {
            if (target == null)
            {
                return null;
            }

            Type type = target.GetType();
            (Type type, string name) key = (type, name);
            if (!Methods.TryGetValue(key, out MethodInfo method))
            {
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                foreach (MethodInfo candidate in type.GetMethods(flags))
                {
                    if (candidate.Name == name && candidate.GetParameters().Length == 1)
                    {
                        method = candidate;
                        break;
                    }
                }

                Methods[key] = method;
            }

            return method?.Invoke(target, new[] { arg });
        }

        private static Rect FullRowRect(Rect row) => new Rect(0, row.y, row.xMax, row.height);

        private static Rect CornerRect(Rect icon)
        {
            float size = Mathf.Lerp(10, 25, Mathf.InverseLerp(16, 64, icon.width));
            return new Rect(icon.xMax - size, icon.yMax - size, size, size);
        }

        private static void DrawIcon(Rect rect, Texture icon, Color color)
        {
            if (!icon)
            {
                return;
            }

            Color old = GUI.color;
            try
            {
                GUI.color *= color;
                GUI.DrawTexture(rect, icon, ScaleMode.ScaleToFit, true);
            }
            finally
            {
                GUI.color = old;
            }
        }

        private static void DrawMinimap(Rect row, float labelStart, string label, FolderContents.Summary contents)
        {
            float labelEnd = labelStart + EditorStyles.label.CalcSize(new GUIContent(label)).x + 13;
            float right = row.xMax - 6;
            // Allocate space before drawing so content order stays stable as the pane resizes.
            int count = Mathf.Min(contents.Icons.Count, Mathf.Max(0, Mathf.FloorToInt((right - labelEnd) / 13)));
            float x = right - count * 13;
            for (int i = 0; i < count; i++)
            {
                DrawIcon(new Rect(x, row.y + (row.height - 12) / 2, 12, 12), Util.GetIcon(contents.Icons[i]), new Color(1, 1, 1, EditorGUIUtility.isProSkin ? .5f : .7f));
                x += 13;
            }
        }

        private static object Member(object target, string name)
        {
            if (target == null)
            {
                return null;
            }

            Type type = target.GetType();
            (Type type, string name) key = (type, name);
            if (!Members.TryGetValue(key, out MemberInfo member))
            {
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                member = (MemberInfo)type.GetField(name, flags) ?? type.GetProperty(name, flags);
                Members[key] = member;
            }

            if (member is FieldInfo field)
            {
                return field.GetValue(target);
            }

            return (member as PropertyInfo)?.GetValue(target);
        }

        [MenuItem("Assets/Saints Project/Edit Appearance", false, 2000)]
        private static void EditSelection()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            // PopupWindow converts its anchor from GUI to screen coordinates itself.
            PopupWindow.Show(new Rect(Event.current?.mousePosition ?? Vector2.zero, Vector2.zero),
                new AssetConfigPopup(AssetDatabase.AssetPathToGUID(path)));
        }

        [MenuItem("Assets/Saints Project/Edit Appearance", true)]
        [MenuItem("Assets/Saints Project/Add Favorite", true)]
        private static bool HasSelection() => Selection.activeObject && EditorUtility.IsPersistent(Selection.activeObject);
        [MenuItem("Assets/Saints Project/Add Favorite", false, 2001)]
        private static void FavoriteSelection()
        {
            foreach (UnityEngine.Object asset in Selection.objects)
            {
                if (EditorUtility.IsPersistent(asset))
                {
                    ProjectConfigStore.AddFavorite(asset);
                }
            }
        }
    }
}
