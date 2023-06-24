using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Commands;

[AddComponentMenu($"UKnack/ICommand/{nameof(InvokeUnityEventOnSOEvent)}")]
public class InvokeUnityEventOnSOEvent : CommandMonoBehaviour
{
    [SerializeField] UnityEvent unityEvent;

    public override void Execute()
    {
        unityEvent?.Invoke();
    }
}