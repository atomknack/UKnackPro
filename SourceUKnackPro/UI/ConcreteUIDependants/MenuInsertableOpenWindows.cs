using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.UI;

[AddComponentMenu($"UKnack/UIDependant/{nameof(MenuInsertableOpenWindows)}")]
public class MenuInsertableOpenWindows : MenuInsertableAbstract
{
    [Serializable]
    public struct ButtonClickToOpenWindow
    {
        public string name;
        public UIWindowAbstract window;
        public int order;
    }
    [SerializeField] private ButtonClickToOpenWindow[] _buttons;
    protected override ButtonClickAction[] GetButtonNames()
    {
        ButtonClickAction[] buttonClickActions = new ButtonClickAction[_buttons.Length];
        for (int i = 0; i< _buttons.Length; i++)
        {
            buttonClickActions[i].name = _buttons[i].name;
            buttonClickActions[i].click = _buttons[i].window.OpenNewModal;
            buttonClickActions[i].order = _buttons[i].order;
        }
        return buttonClickActions;
    }
    protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout)
    {
    }
}
