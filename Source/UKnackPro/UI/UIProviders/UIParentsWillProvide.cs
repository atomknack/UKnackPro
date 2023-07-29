using System;
using System.Collections.Generic;
using UKnack.Common;
using UKnack.UI.ShouldBeInternalButActuallyPublicClasses;

namespace UKnack.UI;

[AddComponentMenu("UKnack/UIProviders/UIParentsWillProvide")]
public sealed class UIParentsWillProvide : UILayoutProviderSorted
{
    private UILayoutProviderSorted provider = null;
    private List<IDependant> pendingRegistration = new();
    private bool visibilityIsSet = false;
    private bool visibilityValue;

    public override float SortingOrder => ZInverseOrderFromProvider(provider);
    public static float ZInverseOrderFromProvider(UILayoutProviderSorted provider)
    {
        if (provider == null)
            throw new InvalidOperationException($"Cannot get Z sorting order when provider not Awaken");
        return provider.SortingOrder;
    }


    internal static UILayoutProviderSorted GetProviderFromParent(GameObject go)
    {
        if (go.transform.parent == null)
            return null;

        return go.transform.parent.gameObject.GetComponentInParent<UILayoutProviderSorted>(includeInactive: true);

    }
    private void Awake()
    {
        provider = GetProviderFromParent(gameObject);
        if (provider == null)
            throw new Exception($"There should be at least one parent with UILayoutProviderAbstract for {CommonStatic.GetFullPath_Recursive(gameObject)}");

        SetVisibility();

        foreach (var pending in pendingRegistration)
            provider.RegisterScript(pending);
    }
    internal override void RegisterScript(IDependant dependant)
    {
        if (provider != null)
        {
            provider.RegisterScript(dependant);
            return;
        }
        pendingRegistration.Add(dependant);
    }

    private void SetVisibility()
    {
        if (visibilityIsSet == false)
            return;
        if (provider != null)
        {
            if(visibilityValue)
                provider.ShouldBeVisible();
            else
                provider.ShouldBeInvisible();
        }
    }

    public override void ShouldBeInvisible()
    {
        visibilityIsSet= true;
        visibilityValue= false;

        SetVisibility();
    }

    public override void ShouldBeVisible()
    {
        visibilityIsSet = true;
        visibilityValue = true;

        SetVisibility();
    }
}
