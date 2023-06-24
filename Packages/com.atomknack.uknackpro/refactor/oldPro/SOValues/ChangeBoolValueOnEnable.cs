using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UKnack.Attributes;
using UKnack.Commands;
using UKnack.Values;
using UnityEditor;
using UnityEngine;

namespace UKnack.SOValues;

[AddComponentMenu($"UKnack/SetValues/{nameof(SetValuesBool)} ICommand")]
public class SetValuesBool : CommandMonoBehaviour
{
    [Serializable]
    public struct ValueStorageAndSetValuePair
    {
        [SerializeField]
        [ValidReference(typeof(ValueStorageAndSetValuePair), nameof(Validate), typeof(IValueSetter<bool>))] 
        public UnityEngine.Object storage;

        [SerializeField] public bool newValue;

        public static IValueSetter<bool> Validate(UnityEngine.Object value)
        {
            IValueSetter<bool> iSetter = value as IValueSetter<bool>;
            if (iSetter == null)
                throw new Exception($"Object type is not IValueSetter<bool>");
            return iSetter;
        }
    }
    [SerializeField] private bool executeOnEnable = true;
    [SerializeField] private ValueStorageAndSetValuePair[] pairs;

    public void OnEnable()
    {
        if (pairs == null)
            throw new ArgumentNullException(nameof(pairs));
        if (executeOnEnable)
            Execute();
    }

    public override void Execute()
    {
        for (int i = 0; i < pairs.Length; ++i)
            ValueStorageAndSetValuePair.Validate(pairs[i].storage).SetValue(pairs[i].newValue);
    }
}

/*
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError("Gameobject disabled, but OnEnable was called. Should never happen");
            // https://docs.unity3d.com/2023.1/Documentation/Manual/ExecutionOrder.html:
            // Awake: This function is always called before any Start functions and also just after a prefab is instantiated.
            // (If a GameObject is inactive during start up Awake is not called until it is made active.)
            return;
        }
*/