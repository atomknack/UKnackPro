using UnityEngine;
using UKnack.Attributes;
using UKnack.Events;

namespace UKnack.Commands;

[AddComponentMenu($"UKnack/ICommand/{nameof(ExecuteAllInChildrenOnSOEvent)}")]
public sealed class ExecuteAllInChildrenOnSOEvent : ExecuteAllInChildren
{
    [SerializeField]
    [ValidReference(typeof(IEvent), nameof(IEvent.Validate))]
    private SOEvent _subscribedTo;

    protected override void OnEnable()
    {
        base.OnEnable();
        IEvent.Validate(_subscribedTo).Subscribe(this);
    }
    private void OnDisable() => _subscribedTo.UnsubscribeNullSafe(this);
}