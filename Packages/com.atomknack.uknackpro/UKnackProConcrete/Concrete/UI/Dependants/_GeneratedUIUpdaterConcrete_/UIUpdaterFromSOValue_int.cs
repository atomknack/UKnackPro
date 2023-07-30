//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from EffortlessUpdaterFromValueSO
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

using UnityEngine;
using System.ComponentModel;
using UKnack.Attributes;
using UKnack.Values;
using UKnack.Preconcrete.UI.SimpleToolkit;

namespace UKnack.Concrete.UI.Dependants
{

    [AddComponentMenu("UKnack/UI.Dependants/UIUpdaterFromSOValue/int")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public sealed class UIUpdaterFromSOValue_int : UIUpdaterFromSOValue<int>
    {
        [SerializeField][ValidReference(nameof(ValidIValue))] private SOValue<int> _value;

        protected override IValue<int> GetValidValueProvider() 
            => ValidIValue(_value);

        protected override string RawValueToStringConversion(int value)
            => value.ToString();
    

        public static IValue<int> ValidIValue(UnityEngine.Object value)
        {
            if (value==null)
                throw new System.ArgumentNullException(nameof(value));
            var asIValue = value as IValue<int>;
            if (asIValue == null)
                throw new System.InvalidCastException($"{value.GetType()} is not compatible with {typeof(IValue<int>)}");
            return asIValue;
        }
    }
}

