using System;
using UKnack.Attributes;
using UKnack.Common;
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
        Action IAskToClose.AskToCloseAction { set { _askToCloseAction = value; Debug.Log(_askToCloseAction); } }

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

        protected virtual void OnValidate()
        {
            //#if UNITY_EDITOR
            try
            {
                (UIDocument document, string buttonIdentificator) = EffortlessUIElement_Button_Fields;
                document = ProvidedComponentAttribute.ProvideOrNull<UIDocument>(gameObject, document);
                if (document == null && gameObject.transform.parent == null)
                    return;
                if (String.IsNullOrWhiteSpace(buttonIdentificator))
                    throw new System.Exception("button identity should not be empty");
                var root = document.visualTreeAsset.Instantiate();
                if (root == null)
                    throw new System.NullReferenceException("UIDocument root visual element is null");
                Button button = root.Q<Button>(buttonIdentificator);
                if (button == null)
                    throw new System.Exception($"No button found with id:{buttonIdentificator} in provided document of {nameof(AskToCloseByEffortlessButton)}");
            } catch (Exception e)
            {
                Debug.Log($"There is error in validation of {CommonStatic.GetFullPath_Recursive(gameObject)}");
                Debug.LogException(e);
            }
//#endif
        }
    }
}
