using UKnack.Preconcrete.UI.Dependants;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UKnack.Concrete.UI.Dependants
{
    [System.Obsolete("not tested")]
    [AddComponentMenu("UKnack/UIDependant/ButtonClickToUnityEvent")]
    public class ButtonClickToUnityEvent : Dependant
    {
        [SerializeField] private string _buttonName;
        [SerializeField] private UnityEvent _buttonClickEvent;
        private Button _button;

        protected override void OnLayoutCreatedAndReady(VisualElement layout)
        {
            _button = AddButtonAction(_buttonName, _buttonClickEvent, layout);
        }
        protected override void OnLayoutGonnaBeDestroyedNow()
        {
            RemoveButtonAction(_buttonName, _button, _buttonClickEvent);
            _button = null;
        }
        public static void RemoveButtonAction(string buttonName, Button button, UnityEvent clickEvent)
        {
            if (button == null)
                throw new System.Exception($"button {buttonName} not found to remove UnityEvent from click events");
            button.clicked -= clickEvent.Invoke;
        }
        public static Button AddButtonAction(string buttonName, UnityEvent clickEvent, VisualElement layout)
        {
            Button button = layout.Q<Button>(buttonName);
            if (button == null)
                throw new System.Exception($"button {buttonName} not found");
            if (clickEvent == null)
                throw new System.ArgumentNullException(nameof(clickEvent));
            button.clicked += clickEvent.Invoke;
            return button;
        }

    }
}