using System;
using System.Collections.Generic;
using System.Text;
using UKnack;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.SOValues;
using UKnack.Values;

namespace UKnackPro.Assorted;

[AddComponentMenu("UKnack/Assorted/EnableDisableGameobjectsOnSOValue")]
public sealed class EnableDisableGameobjectsOnSOValue : AbstractEnableDisableGameobjectsOn
{
    [SerializeField]
    [DisableEditingInPlaymode]
    //[MarkNullAsRed]
    [ValidReference(nameof(ThrowIfNull),typeof(IValue<bool>))] 
    private SOValue<bool> valueToCheck;

    [SerializeField] private bool _runOnEnable = true;

    private static UnityEngine.Object ThrowIfNull(UnityEngine.Object o)
    {
        if (o == null) 
            throw new ArgumentNullException(nameof(o));
        return o;
    }

    private void OnEnable()
    {
        ValidateGameobjects(_objectsToEnableDisable);
        valueToCheck.Subscribe(SetActiveGameobjectsBasedOnValue);
        if (_runOnEnable )
            SetActiveGameobjectsBasedOnValue(valueToCheck.GetValue());
    }
    private void OnDisable()
    {
        valueToCheck.UnsubscribeNullSafe(SetActiveGameobjectsBasedOnValue);
    }
}
