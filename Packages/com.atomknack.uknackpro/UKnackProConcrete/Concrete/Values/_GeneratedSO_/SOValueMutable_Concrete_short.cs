//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Concrete_ValueSO
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

using UnityEngine;
using UKnack.Values;
using UKnack.Preconcrete.Values;

namespace UKnack.Concrete.Values
{

[CreateAssetMenu(fileName = "SOValue_short", menuName = "UKnack/SOValueMutable/short", order = 110)]
public sealed class SOValueMutable_Concrete_short : SOValueMutable<short>
    {
        [SerializeField] 
        private USetOrDefault<short> _value;

        public override short RawValue { get => _value.Value; protected set => _value.Value = value; }


    }

}
