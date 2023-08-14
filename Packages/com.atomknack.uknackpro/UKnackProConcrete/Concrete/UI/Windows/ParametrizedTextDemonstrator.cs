using UKnack.Preconcrete.UI.Windows;
using UKnack.UI.Windows.Aspects;
using UnityEngine;

namespace UKnack.Concrete.UI.Windows
{
    [AddComponentMenu("UKnack/AspectedModals/Parametrized/TextDemonstrator")]
    internal class ParametrizedTextDemonstrator: AspectedModalSingle
    {
        private string _header = null;
        private string _mainBody = null;

        protected override void ValidateAspects(IAspect[] aspects)
        {
            EnsureThereIsOnlyOneOfEachAspectAndNoUncaccountedFor(
                new System.Type[] { typeof(IModal), typeof(IAskToClose), typeof(ITextSetter_HeaderText), typeof(ITextSetter_MainBodyText)},
                TypesOfAspects(aspects)
                );
        }

        public void ShowDemonstratorModal(string header, string mainBody)
        {
            _header = header;
            _mainBody = mainBody;
            ValidateTextFields();
            OpenModal();
            _header = null;
            _mainBody = null;
        }

        public void TryUpdateDemonstratorModal(string header, string mainBody)
        {
            _header = header;
            _mainBody = mainBody;
            ValidateTextFields();
            TryUpdateCurrentModal();
            _header = null;
            _mainBody = null;
        }

        protected override void Init(GameObject opened) 
        {
            ValidateTextFields("This should never happen, "); // redundand validation, could be removed
            GetAspect<ITextSetter_HeaderText>(opened).HeaderText = _header;
            GetAspect<ITextSetter_MainBodyText>(opened).MainBodyText = _mainBody;
            GetAspect<IAskToClose>(opened).AskToCloseAction = CloseModal;
            base.Init(opened);
        }
        private void ValidateTextFields(string exceptionMessagePrefix = null)
        {
            if (_header == null || _mainBody == null)
                throw new System.InvalidOperationException($"{exceptionMessagePrefix}one or both text fields are null, did you forget to set them?");
        }
    }
}
