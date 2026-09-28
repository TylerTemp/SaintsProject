using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SaintsProject.Editor.Config
{
    public class ConfigEditWindow : EditorWindow
    {
        private IConfig _target;
        private bool _refreshing;
        // [MenuItem("Window/Saints/Project/Config")]
        public static void Open() => GetWindow<ConfigEditWindow>("Saints Project Config");
        private void OnEnable() => ProjectConfigStore.Changed += Refresh;
        private void OnDisable() => ProjectConfigStore.Changed -= Refresh;
        public void CreateGUI() => Refresh();
        private void Refresh()
        {
            if (_refreshing)
            {
                return;
            }

            _refreshing = true;
            try
            {
                minSize = new Vector2(360, 240);
                rootVisualElement.Clear();
                _target = ProjectConfigStore.Active;
                Toggle personal = LeftToggle("Enable Personal Config", PersonalProjectConfig.instance.personalEnabled);
                personal.RegisterValueChangedCallback(evt => ProjectConfigStore.Edit(PersonalProjectConfig.instance, "Toggle Personal Project Config", () => PersonalProjectConfig.instance.personalEnabled = evt.newValue));
                rootVisualElement.Add(personal);
                ScrollView fields = new ScrollView
                {
                    style =
                    {
                        flexGrow = 1,
                    },
                };
                rootVisualElement.Add(fields);
                AddToggle(fields, "Disabled", _target.Disabled, value => _target.Disabled = value);
                VisualElement appearance = new VisualElement();
                fields.Add(appearance);
                appearance.SetEnabled(!_target.Disabled);
                AddToggle(appearance, "Background Strip", _target.BackgroundStrip, value => _target.BackgroundStrip = value);
                AddToggle(appearance, "Minimal Mode", _target.MinimalMode, value => _target.MinimalMode = value);
                AddToggle(appearance, "Indent Guides", _target.IndentGuides, value => _target.IndentGuides = value);
                AddToggle(appearance, "Auto Icons", _target.AutoIcons, value => _target.AutoIcons = value);
                AddToggle(appearance, "Content Minimap", _target.ContentMinimap, value => _target.ContentMinimap = value);
                AddToggle(appearance, "Disable Favorites", _target.DisableFavorites, value => _target.DisableFavorites = value);
                VisualElement favorites = new VisualElement();
                appearance.Add(favorites);
                favorites.SetEnabled(!_target.DisableFavorites);
                AddToggle(favorites, "Favorite Click To Inspect", _target.FavoriteClickToInspect, value => _target.FavoriteClickToInspect = value);
                AddToggle(favorites, "Save Favorites To Project Config", _target.SaveFavoritesToProjectConfig, value => _target.SaveFavoritesToProjectConfig = value);
            }
            finally
            {
                _refreshing = false;
            }
        }

        private void AddToggle(VisualElement parent, string label, bool value, Action<bool> setter)
        {
            Toggle toggle = LeftToggle(label, value);
            toggle.RegisterValueChangedCallback(evt => ProjectConfigStore.Edit(_target, "Edit Project Config", () => setter(evt.newValue)));
            parent.Add(toggle);
        }

        private static Toggle LeftToggle(string label, bool value)
        {
            Toggle toggle = new Toggle(label)
            {
                value = value,
                style =
                {
                    flexDirection = FlexDirection.RowReverse,
                    justifyContent = Justify.FlexEnd,
                },
            };
            VisualElement input = toggle.Q(className: Toggle.inputUssClassName);
            input.style.flexGrow = 0;
            input.style.marginRight = 2;
            return toggle;
        }
    }
}
