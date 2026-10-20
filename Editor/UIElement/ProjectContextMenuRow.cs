using System;
using SaintsProject.Editor.Utils;
using UnityEditor;
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

        internal ProjectContextMenuRow(string label, string shortcut, bool isChecked, bool enabled) : this(false)
        {
            this.Q<Label>("label").text = label;
            this.Q<Label>("shortcut").text = shortcut;
            this.Q<Label>("check").text = isChecked ? "✓" : "";
            SetEnabled(enabled);
        }

        internal static ProjectContextMenuRow Separator() => new ProjectContextMenuRow(true);

        internal void SetSelected(bool value) => EnableInClassList("project-context-row--selected", value);
    }
}
