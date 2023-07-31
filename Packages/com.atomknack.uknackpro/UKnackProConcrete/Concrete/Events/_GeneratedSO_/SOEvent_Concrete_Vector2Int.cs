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
    [CreateAssetMenu(fileName = "SOEvent_Vector2Int", menuName = "UKnack/SOEvents/SOEvent_Vector2Int")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    internal sealed class SOEvent_Concrete_Vector2Int : SOEvent<Vector2Int>
    {
        [SerializeField] private UnityEvent<Vector2Int> _beforeSubscribers;
        [SerializeField] private UnityEvent<Vector2Int> _afterSubscribers;

        internal override void InternalInvoke(Vector2Int v)
        {
            _beforeSubscribers.Invoke(v);
            base.InternalInvoke(v);
            _afterSubscribers.Invoke(v);
        }
    }
}

