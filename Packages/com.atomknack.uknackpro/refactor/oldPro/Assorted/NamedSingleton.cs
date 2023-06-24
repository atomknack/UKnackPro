using System;
using System.Collections.Generic;
using UnityEngine;

namespace UKnack.Assorted;

[AddComponentMenu($"UKnack/Assorted/{nameof(NamedSingleton)}")]
[DisallowMultipleComponent]
public sealed class NamedSingleton : MonoBehaviour
{
    private static Dictionary<string, NamedSingleton> instances = new();

    [SerializeField] private string uniqueSingletonName;

    public string GetSingletonInstanceName() => uniqueSingletonName;

    private void Awake()
    {
        if (uniqueSingletonName == null)
            throw new ArgumentNullException(nameof(uniqueSingletonName));
        if (uniqueSingletonName.Length == 0)
            throw new ArgumentException("uniqueSingletonName length should be more than 0");

        if (instances.ContainsKey(uniqueSingletonName))
        {
            Destroy(gameObject);
            return;
        }

        instances.Add(uniqueSingletonName, this);
    }
}
