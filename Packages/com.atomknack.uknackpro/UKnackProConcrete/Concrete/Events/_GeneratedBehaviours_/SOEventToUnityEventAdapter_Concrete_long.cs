//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from SOEventToUnityEventAdapter_FromGeneric
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Preconcrete.Commands;

using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Events
{
    /// <summary>
    /// Subscribes UnityEvent to SOEvent. 
    /// </summary>
    [AddComponentMenu("UKnack/SOEventToUnityEventAdapters/SOEvent_long_toUnityEvent")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEventToUnityEventAdapter_Concrete_long : AbstractCommandSubscribedToSOEvent<long>
    {
        [SerializeField]
        [ValidReference(typeof(IEvent<long>), nameof(IEvent<long>.Validate))] 
        private SOEvent<long> _subscribedTo;

        [SerializeField]
        private UnityEvent<long> _unityEvent;

        protected override IEvent<long> SubscribedTo => 
            IEvent<long>.Validate(_subscribedTo);

        public override void Execute(long num) => 
            _unityEvent?.Invoke(num);

    }
}

