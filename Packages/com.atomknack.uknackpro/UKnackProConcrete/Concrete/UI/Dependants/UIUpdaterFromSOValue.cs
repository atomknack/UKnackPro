using UnityEngine.UIElements;
using UKnack.Values;
using UKnack.Events;
using UKnack.UI;
using UnityEngine;
using UKnack.Preconcrete.UI.Dependants;

namespace UKnack.Concrete.UI.Dependants
{

    public abstract class UIUpdaterFromSOValue<T> : Dependant
    {
        [SerializeField] private string _nameOfUpdatedUI;
        private IValue<T> _counter;
        private VisualElement _currentUpdatedUIElement;
        protected VisualElement GetCurrentUpdatedUIElement => _currentUpdatedUIElement;
        protected abstract IValue<T> GetValidValueProvider();
        protected abstract string RawValueToStringConversion(T value);

        protected override void OnLayoutCreatedAndReady(VisualElement layout)
        {
            _counter = GetValidValueProvider();
            _currentUpdatedUIElement = layout.TryFindSomeKindOfTextStorage(_nameOfUpdatedUI);
            UIContainerValidation(_currentUpdatedUIElement, _nameOfUpdatedUI);
            UpdateUIBasedOnStorage(_counter.GetValue());
            _counter.Subscribe(UpdateUIBasedOnStorage);
        }

        private void UpdateUIBasedOnStorage(T value)
        {
            _currentUpdatedUIElement.TryAssignTextWithoutNotification(RawValueToStringConversion(value));
        }

        protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout)
        {
        }
        protected override void OnLayoutGonnaBeDestroyedNow()
        {
            UIContainerValidation(_currentUpdatedUIElement, _nameOfUpdatedUI);
            _counter.UnsubscribeNullSafe(UpdateUIBasedOnStorage);
        }

        private static void UIContainerValidation(VisualElement storage, string nameOfStorage)
        {
            if (string.IsNullOrEmpty(nameOfStorage))
                throw new System.NullReferenceException($"name of storage is null or empty");
            if (string.IsNullOrWhiteSpace(nameOfStorage))
                throw new System.NullReferenceException($"name of storage is null or whitespaces");
            if (storage == null)
                throw new System.NullReferenceException($"Suitable text storage: {nameOfStorage} not found");
        }
    }
}