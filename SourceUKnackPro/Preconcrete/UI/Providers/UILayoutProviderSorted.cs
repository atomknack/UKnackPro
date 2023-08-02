using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UKnack.UI;

namespace UKnack.Preconcrete.UI.Providers;

[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class UILayoutProviderSorted : LayoutProvider, IUIShouldBeVisible
{
    private static List<UILayoutProviderSorted> s_allProviders = new List<UILayoutProviderSorted>();
    /// <summary>
    /// UIDocument sorting order, bigger it is - the closer it to camera (unlike normal 3d objects).
    /// Document with biggest sorting order is on top of all others
    /// </summary>
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

    protected static void RegisterActive(UILayoutProviderSorted provider) => s_allProviders.Add(provider);
    protected static void UnRegisterInactive(UILayoutProviderSorted provider) => s_allProviders.Remove(provider);
    public static bool TryGetMaxZInversedOrder(out float order)
    {
        if (s_allProviders.Count > 0)
        {
            order = s_allProviders.Max(x => x.SortingOrder);
            return true;
        }
        order = 0f;
        return false;
    }
    public static float NextFreeSpotOnTop()
    {
        const float STEP = 100f;
        if (TryGetMaxZInversedOrder(out float order))
            return order + STEP;
        return 10000f;
    }
}