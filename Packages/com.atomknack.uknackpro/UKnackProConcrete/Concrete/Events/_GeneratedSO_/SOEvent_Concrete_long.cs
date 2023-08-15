//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Concrete_SOEventS
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;
using UKnack.Events;

using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Events
{

    /// This class not intended to be used in code, but only made for ease of creation scriptable object in Unity Editor
    [CreateAssetMenu(fileName = "SOEvent_long", menuName = "UKnack/SOEvents/SOEvent_long")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEvent_Concrete_long : SOEvent<long>
    {
        [SerializeField] private UnityEvent<long> _beforeSubscribers;
        [SerializeField] private UnityEvent<long> _afterSubscribers;

        internal override void InternalInvoke(long num)
        {
            _beforeSubscribers.Invoke(num);
            base.InternalInvoke(num);
            _afterSubscribers.Invoke(num);
        }
    }
}

