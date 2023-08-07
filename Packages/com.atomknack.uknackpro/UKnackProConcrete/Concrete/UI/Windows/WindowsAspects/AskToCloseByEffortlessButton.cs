using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Attributes;
using UKnack.Preconcrete.UI.SimpleToolkit;
using UKnack.UI.Windows;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Concrete.UI.Windows.WindowsAspects
{
    [AddComponentMenu("UKnack/UIWindowAspects/AskToCloseByEffortlessButton")]
    internal class AskToCloseByEffortlessButton : EffortlessUIElement_Button, IAskToClose
    {
        private Action _askToCloseAction;
        Action IAskToClose.AskToCloseAction { set => _askToCloseAction = value; }

        public void AskToClose()
        {
            _askToCloseAction();
        }

        protected override void LayoutCleanupBeforeDestruction()
        {
            _button.clicked -= AskToClose;
        }

        protected override void LayoutReadyAndElementFound(VisualElement layout)
        {
            _button.clicked += AskToClose;
        }

        void IAskToClose.AskToClose()
        {
            throw new NotImplementedException();
        }
    }
}
