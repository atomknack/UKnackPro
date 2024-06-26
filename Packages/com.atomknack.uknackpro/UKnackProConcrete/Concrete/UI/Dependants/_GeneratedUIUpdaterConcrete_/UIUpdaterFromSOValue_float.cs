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

    [AddComponentMenu("UKnack/UI.Dependants/UIUpdaterFromSOValue/float")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public sealed class UIUpdaterFromSOValue_float : UIUpdaterFromSOValue<float>
    {
        [SerializeField][ValidReference(nameof(ValidIValue))] private SOValue<float> _value;

        protected override IValue<float> GetValidValueProvider() 
            => ValidIValue(_value);

        protected override string RawValueToStringConversion(float value)
            => value.ToString();
    

        public static IValue<float> ValidIValue(UnityEngine.Object value)
        {
            if (value==null)
                throw new System.ArgumentNullException(nameof(value));
            var asIValue = value as IValue<float>;
            if (asIValue == null)
                throw new System.InvalidCastException($"{value.GetType()} is not compatible with {typeof(IValue<float>)}");
            return asIValue;
        }
    }
}

