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
    [CreateAssetMenu(fileName = "SOEvent_short", menuName = "UKnack/SOEvents/SOEvent_short")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEvent_Concrete_short : SOEvent<short>
    {
        [SerializeField] private UnityEvent<short> _beforeSubscribers;
        [SerializeField] private UnityEvent<short> _afterSubscribers;

        internal override void InternalInvoke(short num)
        {
            _beforeSubscribers.Invoke(num);
            base.InternalInvoke(num);
            _afterSubscribers.Invoke(num);
        }
    }
}


