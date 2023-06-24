//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from ProUIUpdaterConcrete
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

using UnityEngine;
using System.ComponentModel;
using UKnack.Attributes;
using UKnack.SOValues;
using UKnack.Values;

namespace UKnack.Concrete.UI.Dependants
{

[AddComponentMenu("UKnack/UIDependant/UIUpdater/UIUpdaterFromValueSO_Vector3")]
[EditorBrowsable(EditorBrowsableState.Never)]
[Browsable(false)]
public sealed class UIUpdaterFromSOValue_Vector3 : UIUpdaterFromSOValue<Vector3>
{
    [SerializeField][ValidReference(nameof(ValidIValue))] private SOValue<Vector3> _value;

    protected override IValue<Vector3> GetValueProvider() 
        => ValidIValue(_value);

    protected override string RawValueToStringConversion(Vector3 value)
        => value.ToString();
    

    public static IValue<Vector3> ValidIValue(UnityEngine.Object value)
    {
        if (value==null)
            throw new System.ArgumentNullException(nameof(value));
        var asIValue = value as IValue<Vector3>;
        if (asIValue == null)
            throw new System.InvalidCastException($"{value.GetType()} is not compatible with {typeof(IValue<Vector3>)}");
        return asIValue;
    }
}

}