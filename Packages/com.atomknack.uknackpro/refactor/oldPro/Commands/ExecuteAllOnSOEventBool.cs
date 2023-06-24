using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UKnack.Attributes;
using UKnack.Events;

namespace UKnack.Commands;

[AddComponentMenu($"UKnack/ICommand/{nameof(ExecuteAllOnSOEventBool)}")]
public sealed class ExecuteAllOnSOEventBool : CommandMonoBehaviour<bool>
{
    [SerializeField]
    [ValidReference]
    private SOEvent<bool> _subscribedTo;
    [SerializeField]
    private bool _executeOn;
    [SerializeField]
    private CommandMonoBehaviour[] _commands;

    private void OnEnable()
    {
        if (_subscribedTo == null)
            throw new ArgumentNullException(nameof(_subscribedTo));
        _subscribedTo.Subscribe(Execute);
    }
    private void OnDisable()
    {
        if (_subscribedTo == null)
            throw new ArgumentNullException(nameof(_subscribedTo));
        _subscribedTo.UnsubscribeNullSafe(Execute);
    }

    public override void Execute(bool t)
    {
        if (_commands == null)
            throw new ArgumentNullException(nameof(_commands));
        if (t==_executeOn)
            foreach(var command in _commands)
            {
                command.Execute();
            }
    }
}