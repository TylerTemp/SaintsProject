using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using SaintsProject.Editor.Config;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SaintsProject.Editor
{
    /// <summary>Reserve space for favorites above Unity's IMGUI Project browser.</summary>
    [InitializeOnLoad]
    public static class ProjectWindow
    {
        private const BindingFlags InstanceFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private static readonly Type BrowserType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ProjectBrowser");
        private static readonly Type HostType = typeof(EditorWindow).Assembly.GetType("UnityEditor.HostView");
        private static readonly FieldInfo Parent = typeof(EditorWindow).GetField("m_Parent", InstanceFlags);
        private static readonly FieldInfo Position = typeof(EditorWindow).GetField("m_Pos", InstanceFlags);
        private static readonly FieldInfo OnGUIField = HostType?.GetField("m_OnGUI", InstanceFlags);
        private static readonly PropertyInfo ActualView = HostType?.GetProperty("actualView", InstanceFlags);
        private static readonly Dictionary<EditorWindow, Wrapper> Windows = new Dictionary<EditorWindow, Wrapper>();
        private static double _nextCheck;
        public static EditorWindow Current { get; private set; }

        static ProjectWindow()
        {
            EditorApplication.update += CheckWindows;
            AssemblyReloadEvents.beforeAssemblyReload += RestoreAll;
            EditorApplication.quitting += RestoreAll;
            ProjectConfigStore.Changed += Refresh;
        }

        private static void Refresh()
        {
            foreach (Wrapper wrapper in Windows.Values)
            {
                wrapper.Bar.Invalidate();
            }

            _nextCheck = 0;
        }

        private static void CheckWindows()
        {
            if (EditorApplication.timeSinceStartup < _nextCheck)
            {
                return;
            }

            _nextCheck = EditorApplication.timeSinceStartup + .5;
            if (BrowserType == null || Parent == null || Position == null || OnGUIField == null || ActualView == null)
            {
                return;
            }

            List<EditorWindow> stale = new List<EditorWindow>();
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (KeyValuePair<EditorWindow, Wrapper> pair in Windows)
            {
                if (!pair.Key || Parent.GetValue(pair.Key) != pair.Value.Host || ActualView.GetValue(pair.Value.Host) as EditorWindow != pair.Key)
                {
                    stale.Add(pair.Key);
                }
            }

            foreach (EditorWindow window in stale)
            {
                Windows[window].Restore();
                Windows.Remove(window);
            }

            foreach (Object findWindow in Resources.FindObjectsOfTypeAll(BrowserType))
            {
                EditorWindow window = findWindow as EditorWindow;
                if (window == null)
                {
                    continue;
                }
                object host = Parent.GetValue(window);
                if (host == null || ActualView.GetValue(host) as EditorWindow != window)
                {
                    continue;
                }

                if (OnGUIField.GetValue(host) is not Delegate current)
                {
                    continue;
                }

                if (Windows.TryGetValue(window, out Wrapper existing))
                {
                    if (current == existing.Installed)
                    {
                        continue;
                    }

                    // Another extension may wrap ours. Do not create a recursive chain.
                    if (current.Method.DeclaringType != BrowserType)
                    {
                        continue;
                    }

                    Windows.Remove(window);
                }

                Wrapper wrapper = new Wrapper(window, host, current);
                Windows[window] = wrapper;
                OnGUIField.SetValue(host, wrapper.Installed);
                window.Repaint();
            }
        }

        private static void RestoreAll()
        {
            foreach (Wrapper wrapper in Windows.Values)
            {
                wrapper.Restore();
            }

            Windows.Clear();
        }

        private sealed class Wrapper
        {
            public readonly object Host;
            public readonly Delegate Installed;
            public readonly FavoritesBar Bar = new FavoritesBar();
            private readonly Delegate _original;
            private readonly EditorWindow _window;
            public Wrapper(EditorWindow window, object host, Delegate original)
            {
                _window = window;
                Host = host;
                _original = original;
                Installed = Delegate.CreateDelegate(OnGUIField.FieldType, this, GetType().GetMethod(nameof(Draw), InstanceFlags)!);
            }

            public void Restore()
            {
                if ((Delegate)OnGUIField.GetValue(Host) == Installed)
                {
                    OnGUIField.SetValue(Host, _original);
                }
            }

            private void InvokeOriginal()
            {
                try
                {
                    _original.DynamicInvoke();
                }
                catch (TargetInvocationException exception)when (exception.InnerException != null)
                {
                    ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
                }
            }

            private void Draw()
            {
                EditorWindow previous = Current;
                Current = _window;
                ProjectEntrance.BeginGUI();
                Rect originalPosition = (Rect)Position.GetValue(_window);
                try
                {
                    IConfig config = ProjectConfigStore.Active;
                    if (config.Disabled || config.DisableFavorites)
                    {
                        InvokeOriginal();
                        return;
                    }

                    float height = Bar.OnGUI(originalPosition.width, originalPosition.height * .35f);
                    using (new GUI.GroupScope(new Rect(0, height, originalPosition.width, Mathf.Max(1, originalPosition.height - height))))
                    {
                        Position.SetValue(_window, new Rect(originalPosition.x, originalPosition.y + height, originalPosition.width, Mathf.Max(1, originalPosition.height - height)));
                        InvokeOriginal();
                    }

                    Bar.DrawDragLabel();
                }
                finally
                {
                    Position.SetValue(_window, originalPosition);
                    Current = previous;
                }
            }
        }
    }


}
