using System;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Commands;

/// <summary>
/// useless
/// </summary>
[AddComponentMenu($"UKnack/ICommand/{nameof(InvokeUnityEvent)}")]
[Obsolete("2023.07 this class is probably useless")]
public sealed class InvokeUnityEvent : CommandMonoBehaviour
{
    [SerializeField] private UnityEvent _unityEvent;

    public override void Execute()
    {
        if(_unityEvent != null )
            _unityEvent.Invoke();
    }
}