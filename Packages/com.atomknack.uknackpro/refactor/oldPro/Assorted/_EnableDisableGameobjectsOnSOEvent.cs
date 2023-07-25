// replaced by SetActiveForGameObjects with combination of SOEventToUnityEventAdapter

/*
using System;
using System.Collections.Generic;
using System.Text;
using UKnack;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.SOValues;

namespace UKnackPro.Assorted;

[AddComponentMenu("UKnack/Assorted/EnableDisableGameobjectsOnSOEvent")]
public sealed class EnableDisableGameobjectsOnSOEvent : AbstractEnableDisableGameobjectsOn
{
    [SerializeField][ValidReference] private SOEvent<bool> valueToCheck;
    private void OnEnable()
    {
        ValidateGameobjects(_objectsToEnableDisable);
        valueToCheck.Subscribe(SetActiveGameobjectsBasedOnValue);
    }
    private void OnDisable()
    {
        valueToCheck.UnsubscribeNullSafe(SetActiveGameobjectsBasedOnValue);
    }
}

*/