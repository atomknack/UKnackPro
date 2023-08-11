//Samish as EffortlessMainBodyText, in case of changes consider autogenerating


using UKnack.UI;
using UKnack.UI.Windows.Aspects;
using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Preconcrete.UI.SimpleToolkit;

namespace UKnack.Concrete.UI.Windows.WindowsAspects
{
    [AddComponentMenu("UKnack/UIWindowAspects/EffortlessHeader")]
    internal class EffortlessHeader : EffortlessUIElement_Label, ITextSetter_HeaderText
    {
        private string _text;

        private bool _active;

        string ITextSetter_HeaderText.HeaderText
        {
            set
            {
                _text = value;
                UpdateText();
            }
        }

        private void UpdateText()
        {
            if (_text == null)
                throw new System.ArgumentNullException(nameof(_text));
            if (_active)
                UIStatic.TryAssignTextWithoutNotification(_label, _text);
        }

        protected override void LayoutReadyAndElementFound(VisualElement layout)
        {
            _active = true;
            UpdateText();
        }

        protected override void LayoutCleanupBeforeDestruction()
        {
            _active = false;
            _text = null;
        }
    }
}
