using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UKnack.UI;

namespace UKnack.Preconcrete.UI.Providers;

[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class UILayoutProviderSorted : LayoutProvider, IOnScreenOrder, IUIShouldBeVisible
{
    public abstract float SortingOrder { get; }

    public abstract void ShouldBeVisible();
    public abstract void ShouldBeInvisible();
    public virtual void ShouldBeVisible(bool visible)
    {
        if (visible)
        {
            ShouldBeInvisible();
            return;
        }
        ShouldBeInvisible();
    }
}