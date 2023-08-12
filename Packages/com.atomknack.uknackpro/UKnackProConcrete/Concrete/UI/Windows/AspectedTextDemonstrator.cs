using System.Linq;
using UKnack.Concrete.UI.Windows;
using UKnack.UI.Windows.Aspects;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace UKnack.Concrete.UI.Windows
{
    [AddComponentMenu("UKnack/AspectedModals/AspectedTextDemonstrator(parametrized)")]
    internal class AspectedTextDemonstrator: AspectedPopUp
    {
        private string _header = null;
        private string _mainBody = null;

        public override void ShowPopUp() => throw new System.Exception($"use {nameof(ShowDemonstratorModal)} method to show modal");

        protected override void ValidateAspects(IAspect[] aspects)
        {
            System.Type[] actualAspectTypes = aspects.Select(x => x.GetType()).ToArray();
            EnsureThereIsOnlyOneOfEachAspectAndNoUncaccountedFor(
                new System.Type[] { typeof(IModal), typeof(IAskToClose), typeof(ITextSetter_HeaderText), typeof(ITextSetter_MainBodyText)},
                actualAspectTypes
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

        //protected override void Open()
        //{
        //    ValidateTextFields("Use overload: Open(string, string), ");
        //    base.Open();
        //}

        protected override void Init(GameObject opened) 
        {
            ValidateTextFields("This should never happen, "); // redundand validation, could be removed
            GetAspect<ITextSetter_HeaderText>(opened).HeaderText = _header;
            GetAspect<ITextSetter_MainBodyText>(opened).MainBodyText = _mainBody;

            base.Init(opened);
        }
        private void ValidateTextFields(string exceptionMessagePrefix = null)
        {
            if (_header == null || _mainBody == null)
                throw new System.InvalidOperationException($"{exceptionMessagePrefix}one or both text fields are null, did you forget to set them?");
        }
    }
}
