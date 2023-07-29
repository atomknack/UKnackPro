//__not updated properly if TextAsset _asset set to none in editor  
/*
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using UnityEngine;

namespace UKnack;

[System.Serializable]
public struct TextFromAssetOrString : IValueGetter<string>
{
    [SerializeField] private TextAsset _asset;
    [SerializeField] private string _string;
    public string text { get => GetValue(); }
    public string GetValue()
    {
        ValidateStatic(this);
        if (_asset != null)
            return _asset.text;
        return _string;
    }
    public static void ValidateStatic(TextFromAssetOrString t)
    {
        if (t._asset== null)
        {
            if (t._string == null)
                throw new Exception("textAsset and textAsString should not both be null");
        }
        else
        {
            if (string.IsNullOrEmpty(t._string) == false)
                throw new Exception("there should not be text in String if textAsset is set");
        }
    }
}
*/