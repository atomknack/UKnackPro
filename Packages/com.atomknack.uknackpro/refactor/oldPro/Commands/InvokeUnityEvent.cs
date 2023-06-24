using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Commands;

[AddComponentMenu($"UKnack/ICommand/{nameof(InvokeUnityEvent)}")]
public sealed class InvokeUnityEvent : CommandMonoBehaviour
{
    [SerializeField] private UnityEvent _unityEvent;

    public override void Execute()
    {
        if(_unityEvent != null )
            _unityEvent.Invoke();
    }
}