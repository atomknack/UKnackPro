using System;
using System.Collections;
using System.Collections.Generic;
using UKnack.Attributes;
using UKnack.Events;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace UKnack.Commands;

[AddComponentMenu($"UKnack/ICommand/{nameof(ExecuteAllInChildren)}")]
public class ExecuteAllInChildren : CommandMonoBehaviour, ISubscriberToEvent
{
    [SerializeField]
        [Tooltip("You can self reference gameobject, if you need to run all commands inside this gameobject.")]
            [ValidReference]
            //[MarkNullAsColor(0.1f,0.5f,0.3f, "If target is null, then will be executed all ICommands in this gameobject childs")]
                private GameObject _target;
    [SerializeField]
        [Tooltip("Include ICommand scripts in disabled gameobjects")]
            private bool _includeICommandsInInactiveGameobjects = true;

    public virtual void OnEventNotification() =>
        Execute();

    protected virtual void OnEnable()
    {
        if (_target == null)
            throw new ArgumentNullException(nameof(_target));
    }
    public override void Execute()
    {
        //if (_target != null)
            Execute(_target, _includeICommandsInInactiveGameobjects, this);
        //else
        //    Execute(gameObject, _includeICommandsInInactiveGameobjects, this);
    }
public static void Execute(GameObject go, bool includeCommandsInInactive, ICommand exclude)
    {
        if (go == null)
            throw new ArgumentNullException(nameof(go));
        ICommand[] commands = go.GetComponentsInChildren<ICommand>(includeCommandsInInactive);

        foreach (ICommand command in commands)
            if (exclude != command)
            {
                command.Execute();
            }
        //else
        //    Debug.Log($"Have self in children, total number of commands {commands.Length} with self");
    }
    private static void CanBeNull(UnityEngine.Object obj) { }
}