using System;
using UnityEngine.UIElements;
using UKnack.Attributes;
using UKnack.Events;

namespace UKnack.UI;


//[RequireComponent(typeof(UILayoutProviderAbstract))]
[AddComponentMenu("UKnack/UIDependant/ButtonClickToScriptableObjectEvent")]
public class ButtonClickToSOEvent : Dependant
{
    [SerializeField] string ButtonName;

    [SerializeField, SerializeReference]
    [ValidReference]
    public SOPublisher Publisher;

    private Button _button;
    //private EventCallback<ClickEvent> _eventCallback;

    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
        if (layout == null)
            throw new ArgumentNullException(nameof(layout));
        //Debug.Log($"{ButtonName} of ButtonClickUIDependantPublishToSOEvent started");
        VisualElement rootUI = layout;
        _button = rootUI.Q<Button>(ButtonName);
        if (_button == null)
            throw new Exception($"button {ButtonName} not found");
        if (Publisher == null)
            throw new ArgumentNullException(nameof(Publisher));
        //_eventCallback = ev => Publisher.Publish();
        _button.clicked += Publisher.Publish;//.RegisterCallback<ClickEvent>(_eventCallback);
    }
    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        if (_button == null)
            throw new Exception($"button {ButtonName} not found");
        //_button.UnregisterCallback<ClickEvent>(_eventCallback);
        _button.clicked -= Publisher.Publish;
        _button = null;
        //_eventCallback = null;
    }

    protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout)
    {
    }
}