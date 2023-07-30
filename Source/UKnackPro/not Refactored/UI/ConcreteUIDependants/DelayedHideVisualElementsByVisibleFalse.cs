using System;
using System.Collections;
using System.Collections.Generic;
using UKnack.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class DelayedHideVisualElementsByVisibleFalse : DependantDelayed
{
    [SerializeField] private string[] Elements;

    protected override void AfterAllDependantsCalledAndDelay(VisualElement layout)
    {
        foreach (var element in Elements)
        {
            VisualElement found = layout.Q<VisualElement>(element);
            if (found == null)
                throw new Exception($"VisualElement {found} not found");
            found.visible = false;
        }
    }

    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        //throw new NotImplementedException();
    }
}
