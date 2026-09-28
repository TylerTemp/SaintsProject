using SaintsProject.Editor.UIElement;
using SaintsProject.Editor.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SaintsProject.Editor
{
    public class AssetConfigPopup : PopupWindowContent
    {
        private readonly string _guid;
        private AssetConfig _config;
        public AssetConfigPopup(string guid)
        {
            _guid = guid;
        }

        public override Vector2 GetWindowSize() => new Vector2(200, 200);
        public override void OnGUI(Rect rect)
        {
        }

        public override void OnOpen()
        {
            _config = ProjectConfigStore.GetDirect(_guid);
            VisualElement root = editorWindow.rootVisualElement;
            root.style.paddingTop = 2;
            ColorPickerElement color = new ColorPickerElement();
            color.SetValueWithoutNotify(new ColorPickerResult(_config.hasColor, true, _config.color));
            root.Add(color);
            color.RegisterValueChangedCallback(evt =>
            {
                _config.hasColor = evt.newValue.HasColor;
                _config.color = evt.newValue.Color;
                ProjectConfigStore.SetAsset(_config);
                if (!evt.newValue.HasColor || !evt.newValue.IsCustomColor)
                {
                    editorWindow.Close();
                }
            });
            IconPickerElement icons = new IconPickerElement(_config.icon)
            {
                style =
                {
                    flexGrow = 1,
                    minHeight = 0,
                    marginTop = 4,
                },
            };
            root.Add(icons);
            icons.RegisterValueChangedCallback(evt =>
            {
                _config.icon = Util.NormalizeIcon(evt.newValue);
                ProjectConfigStore.SetAsset(_config);
                editorWindow.Close();
            });
            icons.schedule.Execute(() => icons.Search.Focus());
        }
    }
}
