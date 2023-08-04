using System;
using UKnack.Attributes;
using UKnack.Preconcrete.UI.Dependants;
using UKnack.Preconcrete.UI.Windows;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UKnack.Concrete.UI.Dependants
{
    [System.Obsolete("not tested")]
    [AddComponentMenu("UKnack/UIDependant/MultipleButtonClickToOpenWindow")]
    public class MultipleButtonClickToOpenWindow : Dependant
    {
        [System.Serializable]
        public struct ButtonToOpenWindow
        {
            public string buttonName;
            [ValidReference] public UIWindowScriptableObject window;
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

        protected override void OnLayoutGonnaBeDestroyedNow()
        {
            Validate(_buttonEventPairs);
            for (int i = 0; i < _button.Length; ++i)
            {
                RemoveButtonAction(_buttonEventPairs[i].buttonName, _button[i], _buttonEventPairs[i].window);
                _button[i] = null;
            }
        }

        public static Button AddButtonAction(string buttonName, UIWindowScriptableObject window, VisualElement layout)
        {
            Button button = layout.Q<Button>(buttonName);
            if (button == null)
                throw new Exception($"button {buttonName} not found");
            button.clicked += window.OpenNewModal;
            return button;
        }
        public static void RemoveButtonAction(string buttonName, Button button, UIWindowScriptableObject window)
        {
            if (button == null)
                throw new Exception($"button {buttonName} not found to remove OpenNewWindow from click events");
            button.clicked -= window.OpenNewModal;
        }


        public static void Validate(ButtonToOpenWindow[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));
        }

    }
}
