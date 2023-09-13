using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Preconcrete.Values;
using UKnack.Values;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UKnack.Concrete.Values
{
    [Obsolete("not tested")]
    [CreateAssetMenu(fileName = "SOAnyBoolValueIsTrue", menuName = "UKnack/SOValueDependsOnOther/SOAnyBoolValueIsTrue", order = 110)]
    public class SOAnyBoolValueIsTrue : SOCachedValueOfBoolValuesGroup
    {

        protected override void RefreshCache() // The only method that need to be changed for other condition types
        {
            foreach (var provider in _dependUpon)
                if (provider.GetValue())
                {
                    RawValue = true;
                    return;
                }
            RawValue = false;
        }
    }
}