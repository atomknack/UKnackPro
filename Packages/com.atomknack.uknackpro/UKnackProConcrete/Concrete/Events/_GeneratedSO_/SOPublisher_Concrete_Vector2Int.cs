//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Concrete_SOEventS
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------
using UnityEngine;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Concrete.Values;

using static UnityEngine.InputSystem.InputAction;

namespace UKnack.Concrete.Events
{

/// This class not intended to be used in code, but only made for ease of creation scriptable object in Unity Editor
[CreateAssetMenu(fileName = "PublisherToSOEvent_Vector2Int", menuName = "UKnack/Publishers/To Vector2Int")]
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
internal class SOPublisher_Concrete_Vector2Int : SOPublisher<Vector2Int>
{
    [SerializeField]
    [ValidReference(typeof(IEvent<Vector2Int>), nameof(IEvent<Vector2Int>.Validate),
        typeof(SOEvent<Vector2Int>),
        typeof(SOEvent_Concrete_Vector2Int)
        , typeof(SOValueMutable_Concrete_Vector2Int)
    )] private SOEvent<Vector2Int> where;

    public override void Publish(Vector2Int v)
    {
        ValidateWhere();
        where.InternalInvoke(v);
    }

    internal void ValidateWhere() =>
        IEvent<Vector2Int>.Validate(where);

}

}

