using System;
using UKnack.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UKnack.UI;

//[RequireComponent(typeof(UIDocument))]
[AddComponentMenu($"UKnack/UIDependant/{nameof(MultipleButtonClickToOpenWindow)}")]
public class MultipleButtonClickToOpenWindow : Dependant
{
    [System.Serializable]
    public struct ButtonToOpenWindow
    {
        public string buttonName;
        [ValidReference] public UIWindowAbstract window;
    }

    [SerializeField] private ButtonToOpenWindow[] _buttonEventPairs;
    private Button[] _button;

    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
        Validate(_buttonEventPairs);
        if (_button == null || _button.Length != _buttonEventPairs.Length)
            _button = new Button[_buttonEventPairs.Length];
        for (int i = 0; i < _button.Length; ++i)
        {
            _button[i] = AddButtonAction(_buttonEventPairs[i].buttonName, _buttonEventPairs[i].window, layout);
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
            RemoveButtonAction(_buttonEventPairs[i].buttonName, _button[i], _buttonEventPairs[i].window);
            _button[i] = null;
        }
    }

    public static Button AddButtonAction(string buttonName, UIWindowAbstract window, VisualElement layout)
    {
        Button button = layout.Q<Button>(buttonName);
        if (button == null)
            throw new Exception($"button {buttonName} not found");
        button.clicked += window.OpenNewModal;
        return button;
    }
    public static void RemoveButtonAction(string buttonName, Button button, UIWindowAbstract window)
    {
        if (button == null)
            throw new Exception($"button {buttonName} not found to remove OpenNewWindow from click events");
        button.clicked -= window.OpenNewModal;
    }


    public static void Validate(ButtonToOpenWindow[] elements)
    {
        if (elements == null)
            throw new ArgumentNullException(nameof(elements));
        /*
        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i].window == null)
                throw new Exception($"window for button {elements[i].buttonName} should not be null");
        }
        */
    }

}