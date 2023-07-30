using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UKnack;
using UKnack.Attributes;
using UKnack.UI;
using UnityEngine.Events;

namespace UKnack.UI;

public class ButtonInsertableUnityEvent : ButtonInsertableAbstract
{
    [SerializeField][DisableEditingInPlaymode] protected string _buttonText;
    [SerializeField] private UnityEvent _onClicked;

    protected override string ButtonText => _buttonText;

    private void Awake()
    {
        if (_onClicked == null)
            throw new NullReferenceException(nameof(_onClicked));
    }
    protected override void ButtonClicked()
    {
        _onClicked.Invoke();
    }
}