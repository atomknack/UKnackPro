//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from SOValueToUnityEventAdapter_FromGeneric
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;
using UKnack.Preconcrete.Commands;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Values;

using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Values
{
    /// <summary>
    /// Subscribes UnityEvent to SOValue. 
    /// And in OnEnable invokes UnityEvent with SOValue (Unlike SOEvent version of such script, that only waits for event).
    /// </summary>
    [AddComponentMenu("UKnack/SOValueToUnityEventAdapters/SOValue_long_toUnityEvent")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOValueToUnityEventAdapter_Concrete_long : AbstractCommandSubscribedToSOEvent<long>
    {
        [SerializeField]
        [ValidReference(typeof(IEvent<long>), nameof(IEvent<long>.Validate))] 
        private SOEvent<long> _subscribedTo;

        [SerializeField]
        private UnityEvent<long> _unityEvent;

        [SerializeField]
        [Tooltip("Subscribes UnityEvent to SOValue, OnEnable invokes UnityEvent with value of SOValue")]
        [ValidReference(typeof(IValue<long>), nameof(IValue<long>.Validate))] 
        private SOValue<long> _value;

        private new void OnEnable()
        {
            base.OnEnable();
            _unityEvent?.Invoke(_value.GetValue());
        }

        protected override IEvent<long> SubscribedTo => 
            IEvent<long>.Validate(_subscribedTo);

        public override void Execute(long num) => 
            _unityEvent?.Invoke(num);
    }
}
