using System;
using UKnack.Preconcrete.UI.SimpleToolkit;
using UKnack.UI.Windows.Aspects;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Concrete.UI.Windows.WindowsAspects
{
    [AddComponentMenu("UKnack/UIWindowAspects/AskToCloseByEffortlessButton")]
    internal class AskToCloseByEffortlessButton : EffortlessUIElement_Button, IAskToClose
    {
        private Action _askToCloseAction = null;
        Action IAskToClose.AskToCloseAction { set => _askToCloseAction = value; }

        public void AskToClose()
        {
            _askToCloseAction();
        }

        protected override void LayoutCleanupBeforeDestruction()
        {
            _button.clicked -= AskToClose;
            _askToCloseAction = null;
        }

        protected override void LayoutReadyAndElementFound(VisualElement layout)
        {
            if (_askToCloseAction == null)
                throw new System.Exception($"{nameof(AskToCloseByEffortlessButton)} is not properly initialized");
            _button.clicked += AskToClose;
        }
    }
}
