/* replaced by ToggleEventsForElementsBySetEnabled and DelayedDependantLayoutEvent


using System.Collections;
using System.Collections.Generic;
using UKnack.Preconcrete.UI.Dependants;
using UKnack.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DelayedDisableVisualElements : DependantDelayed
{
    [SerializeField] private string[] Elements;

    protected override void AfterAllDependantsCalledAndDelay(VisualElement layout)
    {
        if (Elements != null)
        {
            foreach (string element in Elements)
            {
                var found = layout.Q<VisualElement>(element);
                if (found != null)
                    found.SetEnabled(false);
            }
        }
    }
    protected override void OnLayoutGonnaBeDestroyedNow()
    {
    }

}
*/
