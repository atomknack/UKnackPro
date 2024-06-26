//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from UIElementToRawSOValueFloatBinding
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Values;
using UKnack.Preconcrete.UI.SimpleToolkit;

namespace UKnack.Concrete.UI.SimpleToolkit
{
    [AddComponentMenu("UKnack/UI.SimpleToolkit/EffortlessToggleToRawSOValueFloatBinding")]
    public class EffortlessToggleToRawSOValueFloatBinding : EffortlessUIElement_Toggle, ISubscriberToEvent<float>
    {
        private string _description = string.Empty;
        public string Description => _description;

        [SerializeField]
        [ValidReference(typeof(SOValueMutable<float>), typeof(Values.SOPrefsAudioVolume))]
        private SOValueMutable<float> _valueProvider;

        [SerializeField] 
        private UnityEvent<bool> _onToggleUIChanged;

        private void OnValueChanged(ChangeEvent<bool> ev)
        {
            if (ev.previousValue == ev.newValue)
                return;
            float rawValue = ev.newValue ? 1f : 0f;
            if (rawValue == _valueProvider.RawValue)
                return;
            _valueProvider.SetValue(rawValue);
            _onToggleUIChanged.Invoke(ev.newValue);
        }

        public void OnEventNotification(float _)
        {
            bool value = Mathf.Abs(_valueProvider.RawValue - 1f) < 0.00001f;//approximal version of: _valueProvider.RawValue == 1f
            if (value == _toggle.value)
                return;
            _toggle.SetValueWithoutNotify(value);
        }

        protected override void LayoutReadyAndElementFound(VisualElement layout)
        {
            _description = $"{nameof(EffortlessToggleToRawSOValueFloatBinding)} of {gameObject.name}";

            if (_valueProvider == null)
                throw new System.ArgumentNullException(nameof(_valueProvider));

            if (_onToggleUIChanged == null)
                throw new System.ArgumentNullException(nameof(_onToggleUIChanged));

            OnEventNotification(_valueProvider.RawValue);
            _toggle.RegisterCallback<ChangeEvent<bool>>(OnValueChanged);
            _valueProvider.Subscribe(this);
        }

        protected override void LayoutCleanupBeforeDestruction()
        {
            _toggle.UnregisterCallback<ChangeEvent<bool>>(OnValueChanged);
            _valueProvider.Unsubscribe(this);
        }
    }
}

