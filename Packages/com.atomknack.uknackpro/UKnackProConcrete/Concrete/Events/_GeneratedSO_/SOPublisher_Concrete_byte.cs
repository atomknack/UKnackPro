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
[CreateAssetMenu(fileName = "PublisherToSOEvent_byte", menuName = "UKnack/Publishers/To byte")]
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public class SOPublisher_Concrete_byte : SOPublisher<byte>
{
    [SerializeField]
    [ValidReference(typeof(IEvent<byte>), nameof(IEvent<byte>.Validate),
        typeof(SOEvent<byte>),
        typeof(SOEvent_Concrete_byte)
        , typeof(SOValueMutable_Concrete_byte)
    )] private SOEvent<byte> where;

    public override void Publish(byte b)
    {
        ValidateWhere();
        where.InternalInvoke(b);
    }

    internal void ValidateWhere() =>
        IEvent<byte>.Validate(where);

}

}

