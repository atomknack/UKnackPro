using UnityEngine;
using UKnack.Commands;
using UnityEngine.UIElements;

namespace UKnack.Concrete.Commands.ScriptableObjects
{
    /// <summary>
    /// Wip, not tested
    /// </summary>
    [System.Obsolete("not tested")]
    [CreateAssetMenu(fileName = "MakeAllUnfocusable", menuName = "UKnack/Commands/FocusableSwitcherForUI")]
    public class SwitchFocusablePropertyForUIElements : CommandScriptableObject<VisualElement>
    {
        [System.Serializable]
        public enum ElementType
        {
            Button,
            Toggle,
            VisualElement
        }
        [SerializeField]
        private ElementType _forEachOf;
        [SerializeField]
        private bool _focusable = false;
        public override void Execute(VisualElement layout)
        {
            if (layout == null)
                return;
            switch (_forEachOf)
            {
                case ElementType.Button:
                    layout.Query<Button>().ForEach(SetFocusable);
                    break;
                case ElementType.Toggle:
                    layout.Query<Toggle>().ForEach(SetFocusable);
                    break;
                case ElementType.VisualElement:
                    layout.Query<VisualElement>().ForEach(SetFocusable);
                    break;
            }

        }

        private void SetFocusable(Focusable x)
        {
            x.focusable = _focusable;
        }
    }
}