// obsolete because of possible combinations SOEventToUnityEventAdapter DelayedUnityEventCommand SetActiveForGameObjects  
/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UKnack.Assorted;

[AddComponentMenu($"UKnack/Assorted/{nameof(EnableGameobjectsDelayed)}")]
public sealed class EnableGameobjectsDelayed : MonoBehaviour
{
    [SerializeField]
        [Min(0)]
            [Tooltip("if delay is zero or less, gameobjects will be enabled immediately OnStart")]
                private float delay;
    [SerializeField] 
        private GameObject[] toBeEnabled;
    void Start()
    {
        if (toBeEnabled == null)
            throw new ArgumentNullException(nameof(toBeEnabled));
        if (delay <= 0)
        {
            Delayed();
            return;
        }

        Invoke(nameof(Delayed), delay);
    }

    private void Delayed()
    {
        foreach (var o in toBeEnabled)
            o.SetActive(true);
    }

}
*/