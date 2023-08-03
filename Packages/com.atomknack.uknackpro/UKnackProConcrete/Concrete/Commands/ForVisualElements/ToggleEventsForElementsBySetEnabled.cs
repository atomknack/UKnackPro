using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Preoncrete.Commands;
using System;

namespace UKnack.Concrete.Commands.ForVisualElements
{
    [Obsolete("not tested")]
    [AddComponentMenu("UKnack/CommandForVisualElements/ToggleEventsForElementsBySetEnabled")]
    public class ToggleEventsForElementsBySetEnabled : ForEachFoundVisualElements
    {
        [SerializeField]
        [Header("To disable elements set to false, to enable to true")]
        [Tooltip(
            @"Disable recieving most events by applying this command with false value,
Enable recieving with true value.

For which events affected by disabling/enabling VisualElements see Unity documentation")]
        private bool _value = false;

        protected override void DoActionForFoundElement(VisualElement found)
        {
            found.SetEnabled(_value);
        }
    }
}


