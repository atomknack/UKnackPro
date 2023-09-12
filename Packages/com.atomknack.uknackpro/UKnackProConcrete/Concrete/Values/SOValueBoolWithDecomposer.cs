using UnityEngine;
using UnityEngine.Events;
using UKnack.Preconcrete.Commands;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Values;


namespace UKnack.Concrete.Values
{
    [AddComponentMenu("UKnack/SOValueToUnityEventAdapters/SOValueBoolWithDecomposer")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOValueBoolWithDecomposer : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("OnEnable invokes UnityEvents with value of SOValue")]
        [ValidReference(typeof(IValue<bool>), nameof(IValue<bool>.Validate))] 
        private SOValue<bool> _value;

        [SerializeField]
        private UnityEvent<bool> _onTrue;

        [SerializeField]
        private UnityEvent<bool> _onFalse;

        [SerializeField]
        private UnityEvent<bool> _inverted;

        [SerializeField]
        private UnityEvent<bool> _unchanged;



        private void OnValue(bool value)
        {
            if (value)
                _onTrue?.Invoke(value);

            if (!value)
                _onFalse?.Invoke(value);

            _inverted?.Invoke(!value);

            _unchanged?.Invoke(value);
        }

        private void OnEnable()
        {
            IEvent<bool>.Validate(_value);
            _value.Subscribe(OnValue);
            OnValue(_value.GetValue());
        }
        private void OnDisable()
        {
            _value.Unsubscribe(OnValue);
        }
    }
}

