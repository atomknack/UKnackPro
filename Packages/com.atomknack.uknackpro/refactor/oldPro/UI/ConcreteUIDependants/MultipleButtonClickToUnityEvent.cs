using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UKnack.UI;

//[RequireComponent(typeof(UIDocument))]
[AddComponentMenu("UKnack/UIDependant/MultipleButtonClickToUnityEvent")]
public class MultipleButtonClickToUnityEvent : Dependant
{
    [System.Serializable]
    public struct ButtonToEvent
    {
        public string buttonName;
        public UnityEvent buttonClickEvent;
    }

    [SerializeField] private ButtonToEvent[] _buttonEventPairs;
    private Button[] _button;

    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
        Validate(_buttonEventPairs);
        if (_button == null || _button.Length != _buttonEventPairs.Length)
            _button = new Button[_buttonEventPairs.Length];
        for (int i = 0; i < _button.Length; ++i)
        {
            _button[i] = ButtonClickToUnityEvent.AddButtonAction(
                _buttonEventPairs[i].buttonName, _buttonEventPairs[i].buttonClickEvent, layout);
        }
    }
    protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout)
    {
    }
    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        Validate(_buttonEventPairs);
        for (int i = 0; i < _button.Length; ++i)
        {
            ButtonClickToUnityEvent.RemoveButtonAction(
                _buttonEventPairs[i].buttonName, _button[i], _buttonEventPairs[i].buttonClickEvent);
            _button[i] = null;
        }
    }

    public static void Validate(ButtonToEvent[] beArray)
    {
        if (beArray == null)
            throw new ArgumentNullException(nameof(beArray));
    }

}