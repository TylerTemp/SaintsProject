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
            Texture2D prefixIcon = null, string prefixText = null) : this(false)
        {
            Label labelElement = this.Q<Label>("label");
            labelElement.text = label;
            VisualElement prefix = null;
            if (prefixIcon)
            {
                prefix = new Image
                {
                    image = prefixIcon,
                    scaleMode = ScaleMode.ScaleToFit,
                    style = { width = 16, height = 16 },
                };
            }
            else if (!string.IsNullOrEmpty(prefixText))
            {
                prefix = new Label(prefixText);
            }
            if (prefix != null)
            {
                prefix.pickingMode = PickingMode.Ignore;
                prefix.style.flexShrink = 0;
                prefix.style.marginRight = 4;
                labelElement.parent.Insert(labelElement.parent.IndexOf(labelElement), prefix);
            }
            this.Q<Label>("shortcut").text = shortcut;
            this.Q<Label>("check").text = isChecked ? "✓" : "";
            SetEnabled(enabled);
        }

        internal static ProjectContextMenuRow Separator() => new ProjectContextMenuRow(true);

        internal void SetSelected(bool value) => EnableInClassList("project-context-row--selected", value);
    }
}
