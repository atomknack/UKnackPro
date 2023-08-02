using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UKnack;
using UKnack.Attributes;
using UKnack.UI;
using UnityEngine.Events;
using UKnack.Events;
using UKnack.Values;

namespace UKnack.UI;

public class ButtonInsertableValueStorageBool : ButtonInsertableAbstract
{
    [SerializeField][DisableEditingInPlaymode] 
    protected string _buttonText;

    [SerializeField][ValidReference] 
    private SOValueMutable<bool> _valueStorage;

    [SerializeField] 
    private string _buttonTextWhenFalse;

    protected override string ButtonText => _buttonText;

    private void Awake()
    {
        if (_valueStorage == null)
            throw new NullReferenceException(nameof(_valueStorage));
    }
    protected override void ButtonClicked()
    {
        _valueStorage.Flip();
    }

    protected void ValueChanged(bool value)
    {
        if (value)
        {
            _button.text = ButtonText;
            return;
        }
        _button.text = _buttonTextWhenFalse;
    }

    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
        base.OnLayoutCreatedAndReady(layout);
        ValueChanged(_valueStorage.GetValue());
        _valueStorage.Subscribe(ValueChanged);
    }

    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        _valueStorage.UnsubscribeNullSafe(ValueChanged);
        base.OnLayoutGonnaBeDestroyedNow();
    }


}