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
[CreateAssetMenu(fileName = "PublisherToSOEvent_long", menuName = "UKnack/Publishers/To long")]
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public class SOPublisher_Concrete_long : SOPublisher<long>
{
    [SerializeField]
    [ValidReference(typeof(IEvent<long>), nameof(IEvent<long>.Validate),
        typeof(SOEvent<long>),
        typeof(SOEvent_Concrete_long)
        , typeof(SOValueMutable_Concrete_long)
    )] private SOEvent<long> where;

    public override void Publish(long num)
    {
        ValidateWhere();
        where.InternalInvoke(num);
    }

    internal void ValidateWhere() =>
        IEvent<long>.Validate(where);

}

}

