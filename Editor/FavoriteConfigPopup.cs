using SaintsProject.Editor.UIElement;
using SaintsProject.Editor.Utils;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SaintsProject.Editor
{
    public class FavoriteConfigPopup : PopupWindowContent
    {
        private AssetFavorite _favorite;
        private static VisualTreeAsset _template;
        private float _height = 380;
        public FavoriteConfigPopup(AssetFavorite favorite)
        {
            _favorite = favorite;
        }

        public override Vector2 GetWindowSize() => new Vector2(200, _height);
        public override void OnGUI(Rect rect)
        {
        }

        public override void OnOpen()
        {
            if (!_template)
            {
                _template = Util.LoadResource<VisualTreeAsset>("UIToolkit/FavoriteConfig.uxml");
            }
            TemplateContainer template = _template.CloneTree();
            VisualElement root = template.Q<VisualElement>("favoriteConfig");
            TextField alias = root.Q<TextField>("aliasInput");
            alias.SetValueWithoutNotify(_favorite.alias ?? "");
            EnumField colorType = root.Q<EnumField>("colorType");
            colorType.Init(_favorite.colorType);
            ColorPickerElement colors = root.Q<ColorPickerElement>();
            colors.NoDeleteButton();
            colors.SetValueWithoutNotify(new ColorPickerResult(true, false, _favorite.color));
            EnumField iconType = root.Q<EnumField>("iconType");
            iconType.Init(_favorite.iconType);
            IconPickerElement icons = root.Q<IconPickerElement>();
            icons.SetValueWithoutNotify(_favorite.icon);

            iconType.RegisterValueChangedCallback(_ => RefreshFields());
            colorType.RegisterValueChangedCallback(_ => RefreshFields());
            RefreshFields();
            root.Q<Button>("deleteButton").clicked += () =>
            {
                ProjectConfigStore.RemoveFavorite(_favorite);
                editorWindow.Close();
            };
            root.Q<Button>("saveButton").clicked += Save;
            alias.RegisterCallback<KeyDownEvent>(evt =>
            {
                if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
                {
                    Save();
                    evt.StopPropagation();
                }
            }, TrickleDown.TrickleDown);
            root.RegisterCallback<AttachToPanelEvent>(_ => alias.Focus());
            root.RegisterCallback<GeometryChangedEvent>(_ =>
            {
                float height = root.resolvedStyle.height;
                if (!float.IsNaN(height) && height > 0 && !Mathf.Approximately(_height, height))
                {
                    _height = height;
                    editorWindow.Repaint();
                }
            });
            editorWindow.rootVisualElement.Add(template);
            return;

            void Save()
            {
                _favorite.alias = alias.value;
                _favorite.iconType = (FavoriteIconType)iconType.value;
                _favorite.icon = Util.NormalizeIcon(icons.value);
                _favorite.colorType = (FavoriteColorType)colorType.value;
                if (_favorite.colorType == FavoriteColorType.CustomColor && !colors.value.HasColor)
                {
                    _favorite.colorType = FavoriteColorType.NoColor;
                }

                _favorite.color = colors.value.Color;
                ProjectConfigStore.SetFavorite(_favorite);
                editorWindow.Close();
            }

            void RefreshFields()
            {
                bool custom = (FavoriteIconType)iconType.value == FavoriteIconType.Custom;
                icons.style.display = custom ? DisplayStyle.Flex : DisplayStyle.None;
                colors.style.display = (FavoriteColorType)colorType.value == FavoriteColorType.CustomColor ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }
    }
}
