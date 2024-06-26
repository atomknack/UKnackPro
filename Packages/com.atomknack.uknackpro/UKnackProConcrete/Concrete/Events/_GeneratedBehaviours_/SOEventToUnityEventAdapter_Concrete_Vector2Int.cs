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
    [AddComponentMenu("UKnack/SOEventToUnityEventAdapters/SOEvent_Vector2Int_toUnityEvent")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEventToUnityEventAdapter_Concrete_Vector2Int : AbstractCommandSubscribedToSOEvent<Vector2Int>
    {
        [SerializeField]
        [ValidReference(typeof(IEvent<Vector2Int>), nameof(IEvent<Vector2Int>.Validate))] 
        private SOEvent<Vector2Int> _subscribedTo;

        [SerializeField]
        private UnityEvent<Vector2Int> _unityEvent;

        protected override IEvent<Vector2Int> SubscribedTo => 
            IEvent<Vector2Int>.Validate(_subscribedTo);

        public override void Execute(Vector2Int v) => 
            _unityEvent?.Invoke(v);

    }
}

