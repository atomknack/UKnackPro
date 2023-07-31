using System;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UKnack.UI;

[AddComponentMenu("UKnack/UIDependant/ButtonOverOutToUnityEvents")]
public class ButtonOverOutToUnityEvents : Dependant
{
    public string ButtonName;

    public UnityEvent _buttonOver;
    public UnityEvent _buttonOut;
    private Button _button;

    private EventCallback<MouseEnterEvent> buttonEnterCallback;
    private EventCallback<MouseLeaveEvent> buttonLeaveCallback;

    private void Init(VisualElement layout)
    {
        _button = layout.Q<Button>(ButtonName);
        if (_button == null)
            throw new ArgumentException($"not found button {ButtonName} in {layout.name}, init of gameobject:{gameObject.name}");
        buttonEnterCallback = ev => _buttonOver.Invoke();
        buttonLeaveCallback = ev => _buttonOut.Invoke();
    }

    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
        Init(layout);

        _button.RegisterCallback<MouseEnterEvent>(buttonEnterCallback);
        _button.RegisterCallback<MouseLeaveEvent>(buttonLeaveCallback);
    }
    protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout)
    {
    }

    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        _button.UnregisterCallback<MouseEnterEvent>(buttonEnterCallback);
        _button.UnregisterCallback<MouseLeaveEvent>(buttonLeaveCallback);
    }


}
