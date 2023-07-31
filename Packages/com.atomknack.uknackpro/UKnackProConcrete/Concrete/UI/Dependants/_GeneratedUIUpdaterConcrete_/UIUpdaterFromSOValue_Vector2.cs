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

    [AddComponentMenu("UKnack/UI.Dependants/UIUpdaterFromSOValue/Vector2")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public sealed class UIUpdaterFromSOValue_Vector2 : UIUpdaterFromSOValue<Vector2>
    {
        [SerializeField][ValidReference(nameof(ValidIValue))] private SOValue<Vector2> _value;

        protected override IValue<Vector2> GetValidValueProvider() 
            => ValidIValue(_value);

        protected override string RawValueToStringConversion(Vector2 value)
            => value.ToString();
    

        public static IValue<Vector2> ValidIValue(UnityEngine.Object value)
        {
            if (value==null)
                throw new System.ArgumentNullException(nameof(value));
            var asIValue = value as IValue<Vector2>;
            if (asIValue == null)
                throw new System.InvalidCastException($"{value.GetType()} is not compatible with {typeof(IValue<Vector2>)}");
            return asIValue;
        }
    }
}
