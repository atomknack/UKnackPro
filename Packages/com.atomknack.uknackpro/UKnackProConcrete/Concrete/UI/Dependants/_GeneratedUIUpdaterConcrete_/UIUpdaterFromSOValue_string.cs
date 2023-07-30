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

    [AddComponentMenu("UKnack/UI.Dependants/UIUpdaterFromSOValue/string")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public sealed class UIUpdaterFromSOValue_string : UIUpdaterFromSOValue<string>
    {
        [SerializeField][ValidReference(nameof(ValidIValue))] private SOValue<string> _value;

        protected override IValue<string> GetValidValueProvider() 
            => ValidIValue(_value);

        protected override string RawValueToStringConversion(string value)
            => value.ToString();
    

        public static IValue<string> ValidIValue(UnityEngine.Object value)
        {
            if (value==null)
                throw new System.ArgumentNullException(nameof(value));
            var asIValue = value as IValue<string>;
            if (asIValue == null)
                throw new System.InvalidCastException($"{value.GetType()} is not compatible with {typeof(IValue<string>)}");
            return asIValue;
        }
    }
}

