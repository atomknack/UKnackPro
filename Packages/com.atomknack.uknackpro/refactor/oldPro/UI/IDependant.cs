using UnityEngine.UIElements;

namespace UKnack.UI;

public interface IDependant: ILayoutDependant
{
    // should be called after LayoutReady and before LayoutGonnaBeDestroyedNow
    abstract void LayoutReadyAndAllDependantsCalled(VisualElement layout);

}