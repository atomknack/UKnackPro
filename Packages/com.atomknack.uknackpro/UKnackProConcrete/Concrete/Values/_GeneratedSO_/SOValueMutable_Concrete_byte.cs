//----------------------------------------------------------------------------------------
// <auto-generated> This code was generated from Concrete_ValueSO
// Changes will be lost if the code is regenerated.</auto-generated>
//----------------------------------------------------------------------------------------

using UnityEngine;
using UKnack.Values;
using UKnack.Preconcrete.Values;

namespace UKnack.Concrete.Values
{

[CreateAssetMenu(fileName = "SOValue_byte", menuName = "UKnack/SOValueMutable/byte", order = 110)]
public sealed class SOValueMutable_Concrete_byte : SOValueMutable<byte>
    {
        [SerializeField] 
        private USetOrDefault<byte> _value;

        public override byte RawValue { get => _value.Value; protected set => _value.Value = value; }


    }

}