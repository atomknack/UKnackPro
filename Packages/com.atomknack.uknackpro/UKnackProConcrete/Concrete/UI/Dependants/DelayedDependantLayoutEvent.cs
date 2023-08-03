using UnityEngine.UIElements;
using UKnack.Preconcrete.UI.Dependants;
using UnityEngine.Events;

namespace UKnack.Concrete.UI.Dependants
{
    [System.Obsolete("not tested")]
    public class DelayedDependantLayoutEvent : DependantDelayed
    {
        UnityEvent<VisualElement> _delayedLayoutEvent;

        protected override void AfterAllDependantsCalledAndDelay(VisualElement layout)
        {
            if (layout == null)
                throw new System.ArgumentNullException(nameof(layout));
            if (_delayedLayoutEvent == null)
                throw new System.ArgumentNullException(nameof(_delayedLayoutEvent));
            _delayedLayoutEvent.Invoke(layout);
        }

        protected override void OnLayoutGonnaBeDestroyedNow() { }
    }
}
