using System;
using System.Collections.Generic;
using System.ComponentModel;
using UKnack.UI;
using UnityEngine.UIElements;

namespace UKnack.Preconcrete.UI.Providers;

[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class UILayoutProviderDependantRegisterer : UILayoutProviderSorted
{
    private protected List<ILayoutDependant> _dependants = new List<ILayoutDependant>();
    private protected VisualElement _root;

    protected void RemoveNullDependants()
    {
        int i = 0;
        while (i < _dependants.Count)
        {
            if (_dependants[i] == null)
            {
                _dependants.RemoveAt(i);
                continue;
            }
            i++;
        }
    }

    internal override void RegisterScript(ILayoutDependant unRegisteredDependant)
    {
        //Debug.Log($"Register script called for {unRegisteredDependant}");
        if (unRegisteredDependant == null)
            throw new ArgumentNullException(nameof(unRegisteredDependant));

        foreach (var registered in _dependants)
            if (registered == unRegisteredDependant)
                throw new InvalidOperationException($"Provider already contains registered {unRegisteredDependant}");

        AddDependantToList(unRegisteredDependant);
    }

    private protected virtual void AddDependantToList(ILayoutDependant dependant)
    {
        bool activated = NewDependantCanBeCalled();
        if (activated)
        {
            dependant.LayoutReady(_root);
            dependant.LayoutReadyAndAllDependantsCalled(_root);
        }
        _dependants.Add(dependant);
    }

    private protected virtual bool NewDependantCanBeCalled() => isActiveAndEnabled;
}