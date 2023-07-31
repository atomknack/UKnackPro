/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

namespace UKnack.SOValues;

[AddComponentMenu("UKnack/SOValues/ChangeBoolValueOnAwake")]
public class ChangeBoolValueOnAwake : MonoBehaviour
{
    [Serializable]
    public struct ValueStorageAndSetValuePair
    {
        [SerializeField] public SOValue_bool storage;
        [SerializeField] public bool newValue;
    }

    [SerializeField] private ValueStorageAndSetValuePair[] pairs;

    public void Awake()
    {
        if (pairs == null)
            throw new ArgumentNullException(nameof(pairs));

        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError("Gameobject disabled, but awake was called. Should never happen");
            // https://docs.unity3d.com/2023.1/Documentation/Manual/ExecutionOrder.html:
            // Awake: This function is always called before any Start functions and also just after a prefab is instantiated.
            // (If a GameObject is inactive during start up Awake is not called until it is made active.)
            return;
        }

        for (int i = 0; i < pairs.Length; ++i)
        {
            if (pairs[i].storage == null)
                throw new ArgumentNullException($"pairs {i} storage is NULL (not set)");
            pairs[i].storage.SetValue(pairs[i].newValue);
        }
    }
}
*/