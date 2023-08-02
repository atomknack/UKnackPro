using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UKnack;
using UKnack.UI;
using UKnack.Preconcrete.UI.Dependants;

public class DelayedHideVisualElementsByDisplayNone : DependantDelayed
{
    [SerializeField] private string[] Elements;

    protected override void AfterAllDependantsCalledAndDelay(VisualElement layout)
    {
        foreach (var element in Elements)
        {
            VisualElement found = layout.Q<VisualElement>(element);
            if (found == null)
                throw new Exception($"VisualElement {found} not found");
            found.style.display = DisplayStyle.None;
        }
    }

    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        //throw new NotImplementedException();
    }
}
