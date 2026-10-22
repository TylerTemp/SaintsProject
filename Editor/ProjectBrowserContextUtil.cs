using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace SaintsProject.Editor
{
    public static class ProjectBrowserContextUtil
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

        public readonly struct MenuItem
        {
            public readonly string Path;
            public readonly string Label;
            public readonly string Shortcut;
            public readonly int Priority;
            public readonly bool IsSeparator;

            public MenuItem(string path, string label, string shortcut, int priority, bool isSeparator)
            {
                Path = path;
                Label = label;
                Shortcut = shortcut;
                Priority = priority;
                IsSeparator = isSeparator;
            }
        }

        private static Func<string, bool, bool, Array> _getItems;
        private static Func<string, string> _getHotkey;
        private static bool _getMenuItemsInit;

        public static IEnumerable<MenuItem> GetMenuItems()
        {
            if (!_getMenuItemsInit)
            {
                _getItems = (Func<string, bool, bool, Array>)typeof(Menu).GetMethod("GetMenuItems", Flags)!.CreateDelegate(typeof(Func<string, bool, bool, Array>));

                _getHotkey = (Func<string, string>)typeof(Menu).GetMethod("GetHotkey", Flags)!.CreateDelegate(typeof(Func<string, string>));
                _getMenuItemsInit = true;
            }

            Array items = _getItems("Assets", true, false);
            Array localized = _getItems("Assets", true, true);
            if (items == null || localized == null || items.Length != localized.Length)
            {
                throw new InvalidOperationException("Menu metadata changed during enumeration.");
            }

            for (int i = 0; i < items.Length; i++)
            {
                object item = items.GetValue(i);
                string path = (string)Get(item, "path");
                if (path != "Assets" && !path.StartsWith("Assets/", StringComparison.Ordinal))
                {
                    continue;
                }
                string displayPath = (string)Get(localized.GetValue(i), "path");
                yield return new MenuItem(
                    path,
                    displayPath[(displayPath.LastIndexOf('/') + 1)..],
                    _getHotkey?.Invoke(path),
                    (int)Get(item, "priority"),
                    (bool)Get(item, "isSeparator")
                );
            }
        }

        private static object Get(object target, string name)
        {
            Type type = target.GetType();
            return type.GetField(name, Flags)?.GetValue(target)
                   ?? type.GetProperty(name, Flags)?.GetValue(target);
        }
    }
}
