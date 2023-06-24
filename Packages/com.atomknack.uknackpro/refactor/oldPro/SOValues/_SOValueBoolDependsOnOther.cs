/*
using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.SOValues;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UKnack.Values;

[Obsolete("new version (04.24) not tested")]
[CreateAssetMenu(fileName = "ValueBoolDependsOnOther", menuName = "UKnack/ValueSO/ValueBoolDependsOnOther", order = 110)]
public partial class SOValueBoolDependsOnOther : SOValueMutable<bool>
{
    [SerializeField]
    private USetOrDefault<bool> _value;
    public override bool RawValue { get => _value.Value; set => _value.Value = value; }

    [FormerlySerializedAs("dependUpon")]
    [SerializeField][ValidReference] private SOValue<bool> _dependUpon;

    [NonSerialized] private bool _subscribedTo_dependUpon = false;

    public void Flip() => ExtensionsForSOValues.Flip(this);
    public override bool GetValue()
    {
        if(base.GetValue() == false)
            return false;
        if (_dependUpon == null)
            return true;

        return _dependUpon.GetValue();
    }

    private void ProviderValueChanged(bool providerValue)
    {
        if (providerValue == false)
        {
            //before refactoring: SOEventInternal<bool>.InvokeSubscribers(this, false);
            InvokeSubscribers(this, false);
            return;
        }

        //before refactoring:SOEventInternal<bool>.InvokeSubscribers(this, base.GetValue());
        InvokeSubscribers(this, base.GetValue());
    }

    protected void TrySubscribeToProvider()
    {
        if (_subscribedTo_dependUpon == true)
            return;
        if (_dependUpon == null)
            return;
        _dependUpon.Subscribe(ProviderValueChanged);
        _subscribedTo_dependUpon = true;
    }


    protected void TryUnSubscribeFromProvider()
    {
        if (_subscribedTo_dependUpon == false)
            return;
        if (_dependUpon == null)
            return;
        if (SubscribersCount() == 0 ) 
        {
            _dependUpon.UnsubscribeNullSafe(ProviderValueChanged);
            _subscribedTo_dependUpon = false;
        }
    }

    public override void Subscribe(Action<bool> subscriber)
    {
        TrySubscribeToProvider();
        base.Subscribe(subscriber);
    }
    public override void Subscribe(UnityEvent<bool> subscriber)
    {
        TrySubscribeToProvider();
        base.Subscribe(subscriber);
    }
    public override void Subscribe(ISubscriberToEvent<bool> subscriber)
    {
        TrySubscribeToProvider();
        base.Subscribe(subscriber);
    }

    internal override void Unsubscribe(Action<bool> subscriber)
    {
        base.Unsubscribe(subscriber);
        TryUnSubscribeFromProvider();
    }
    internal override void Unsubscribe(UnityEvent<bool> subscriber)
    {
        base.Unsubscribe(subscriber);
        TryUnSubscribeFromProvider();
    }
    internal override void Unsubscribe(ISubscriberToEvent<bool> subscriber)
    {
        base.Unsubscribe(subscriber);
        TryUnSubscribeFromProvider();
    }

}
*/