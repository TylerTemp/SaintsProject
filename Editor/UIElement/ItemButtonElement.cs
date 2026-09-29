using SaintsProject.Editor.Utils;
using UnityEngine.UIElements;

namespace SaintsProject.Editor.UIElement
{
#if UNITY_6000_0_OR_NEWER
    [UxmlElement]
#endif
    public partial class ItemButtonElement : VisualElement
    {
#if !UNITY_6000_0_OR_NEWER
        public new class UxmlFactory : UxmlFactory<ItemButtonElement, UxmlTraits>
        {
        }

#endif
        private static VisualTreeAsset _template;
        public readonly Button Button;
        public ItemButtonElement()
        {
            if (!_template)
            {
                _template = Util.LoadResource<VisualTreeAsset>("UIToolkit/ItemButton.uxml");
            }
            TemplateContainer root = _template.CloneTree();
            Add(root);
            Button = root.Q<Button>();
        }

        public void SetSelected(bool selected)
        {
            const string className = "ItemButtonSelected";
            if (selected)
            {
                Button.AddToClassList(className);
            }
            else
            {
                Button.RemoveFromClassList(className);
            }
        }
    }
}
