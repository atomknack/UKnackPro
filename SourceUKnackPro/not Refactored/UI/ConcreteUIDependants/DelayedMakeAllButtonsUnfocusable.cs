using UKnack.Preconcrete.UI.Dependants;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.UI;

public class DelayedMakeAllButtonsUnfocusable : DependantDelayed
{
    protected override void AfterAllDependantsCalledAndDelay(VisualElement layout)
    {
        //Debug.Log($"{gameObject.name} MakeAllButtonsUnfocusable with delay {_delay} started for {layout}");
        if (layout == null)
            return;
        layout.Query<Button>().ForEach(x => x.focusable = false);
        //Debug.Log($"Unfocusable set for {layout.Query<Button>().ToList().Count} buttons");
    }

    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        //throw new System.NotImplementedException();
    }

}
