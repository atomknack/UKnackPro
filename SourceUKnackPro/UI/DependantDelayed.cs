using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Attributes;

namespace UKnack.UI;


public abstract class DependantDelayed : Dependant
{
    [SerializeField][Range(0, 100)] protected internal float _delay = 0.1f;
    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
    }
    protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout)
    {
        Debug.Log("Delayed script started after all Dependants called");
        if (_delay > 0)
            StartCoroutine(DoDelayedWork(layout));
        else
            AfterAllDependantsCalledAndDelay(layout);
    }
    private IEnumerator DoDelayedWork(VisualElement layout)
    {
        //yield return null;
        //if (_delay > 0)
        yield return new WaitForSeconds(_delay);

        AfterAllDependantsCalledAndDelay(layout);
    }

    protected abstract void AfterAllDependantsCalledAndDelay(VisualElement layout);
}