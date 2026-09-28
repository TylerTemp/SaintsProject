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
        public FavoriteConfigPopup(AssetFavorite favorite)
        {
            _favorite = favorite;
        }

        public override Vector2 GetWindowSize() => new Vector2(280, 380);
        public override void OnGUI(Rect rect)
        {
        }

        public override void OnOpen()
        {
            VisualElement root = editorWindow.rootVisualElement;
            TextField alias = new TextField("Alias")
            {
                value = _favorite.alias ?? "",
            };
            root.Add(alias);
            EnumField iconType = new EnumField("Icon", _favorite.iconType);
            root.Add(iconType);
            IconPickerElement icons = new IconPickerElement(_favorite.icon)
            {
                style =
                {
                    flexGrow = 1,
                    minHeight = 0,
                },
            };
            root.Add(icons);
            EnumField colorType = new EnumField("Color", _favorite.colorType);
            root.Add(colorType);
            ColorPickerElement colors = new ColorPickerElement();
            colors.SetValueWithoutNotify(new ColorPickerResult(true, true, _favorite.color));
            root.Add(colors);

            iconType.RegisterValueChangedCallback(_ => RefreshFields());
            colorType.RegisterValueChangedCallback(_ => RefreshFields());
            RefreshFields();
            root.Add(new Button(() =>
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
            })
            {
                text = "Save",
            });
            root.Add(new Button(() =>
            {
                ProjectConfigStore.RemoveFavorite(_favorite);
                editorWindow.Close();
            })
            {
                text = "Remove Favorite",
            });
            return;

            void RefreshFields()
            {
                bool custom = (FavoriteIconType)iconType.value == FavoriteIconType.Custom;
                icons.style.display = custom ? DisplayStyle.Flex : DisplayStyle.None;
                colors.style.display = (FavoriteColorType)colorType.value == FavoriteColorType.CustomColor ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }
    }
}
