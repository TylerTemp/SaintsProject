using System;
using SaintsProject.Editor.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SaintsProject.Editor.UIElement
{
    internal sealed class ProjectContextMenuRow : VisualElement
    {
        private ProjectContextMenuRow(bool separator)
        {
            VisualTreeAsset template = Util.LoadResource<VisualTreeAsset>("UIToolkit/ProjectContextMenuRow.uxml");
            template.CloneTree(this);
            AddToClassList("project-context-row");
            EnableInClassList("project-context-row--dark", EditorGUIUtility.isProSkin);
            EnableInClassList("project-context-row--separator", separator);
            SetEnabled(!separator);
        }

        internal ProjectContextMenuRow(string label, string shortcut, bool isChecked, bool enabled,
            Texture2D prefixIcon, string prefixText) : this(false)
        {
            VisualElement iconContainer = this.Q<VisualElement>("icon");
            Label iconText = iconContainer.Q<Label>("iconText");
            Image iconImage = iconContainer.Q<Image>("iconImage");
            Image iconChecked = iconContainer.Q<Image>("iconChecked");
            if (!string.IsNullOrEmpty(prefixText))
            {
                iconText.text = prefixText;
                iconText.style.display = DisplayStyle.Flex;

                iconImage.style.display = DisplayStyle.None;

                iconChecked.style.display = isChecked ? DisplayStyle.Flex : DisplayStyle.None;
            }
            else if (prefixIcon != null)
            {
                iconText.style.display = DisplayStyle.None;

                iconImage.image = prefixIcon;
                iconImage.style.display = DisplayStyle.Flex;

                iconChecked.style.display = isChecked ? DisplayStyle.Flex : DisplayStyle.None;
            }
            else if (isChecked)
            {
                iconText.style.display = DisplayStyle.None;

                iconImage.image = Util.LoadResource<Texture2D>("check");
                iconImage.tintColor = new Color(50 / 255f, 205 / 255f, 50 / 255f);
                iconImage.style.display = DisplayStyle.Flex;

                iconChecked.style.display = DisplayStyle.None;
            }
            else
            {
                iconText.style.display = iconText.style.display = iconChecked.style.display = DisplayStyle.None;
            }
            Label labelElement = this.Q<Label>("label");
            labelElement.text = label;

            this.Q<Label>("shortcut").text = shortcut;
            SetEnabled(enabled);
        }

        internal static ProjectContextMenuRow Separator() => new ProjectContextMenuRow(true);

        internal void SetSelected(bool value) => EnableInClassList("project-context-row--selected", value);
    }
}
