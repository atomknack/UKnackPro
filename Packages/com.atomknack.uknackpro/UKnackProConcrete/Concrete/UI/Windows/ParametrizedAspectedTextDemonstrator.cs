
using UKnack.Concrete.UI.Windows;
using UKnack.UI.Windows.Aspects;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace UKnack.Concrete.UI.Windows
{
    [CreateAssetMenu(fileName = "ParametrizedTextDemonstrator", menuName = "UKnack/AspectedModals/ParametrizedTextDemonstrator", order = 700)]
    internal class ParametrizedAspectedTextDemonstrator: AspectedPopUp
    {
        private string _header = null;
        private string _mainBody = null;

        public void Open(string header, string mainBody)
        {
            _header = header;
            _mainBody = mainBody;
            ValidateTextFields();
            Open();
        }
        public override void Open()
        {
            ValidateTextFields("Use overload: Open(string, string), ");
            base.Open();
        }

        protected override void Init(GameObject opened) 
        {
            ValidateTextFields("This should never happen, ");
            base.Init(opened);
            GetAspect<ITextSetter_HeaderText>(opened).HeaderText = _header;
            GetAspect<ITextSetter_MainBodyText>(opened).MainBodyText = _mainBody;
            _header = null;
            _mainBody = null;
        }
        protected override void ValidateNotNullPrefab(GameObject prefab)
        {
            base.ValidateNotNullPrefab(prefab);
            ValidateAspect<ITextSetter_HeaderText>(prefab);
            ValidateAspect<ITextSetter_MainBodyText>(prefab);
        }
        private void ValidateTextFields(string exceptionMessagePrefix = null)
        {
            if (_header == null || _mainBody == null)
                throw new System.InvalidOperationException($"{exceptionMessagePrefix}one or both text fields are null, did you forget to set them?");
        }
    }
}
