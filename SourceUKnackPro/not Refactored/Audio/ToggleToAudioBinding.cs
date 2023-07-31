using System;
using UKnack.Attributes;
using UKnack.UI;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UKnack.Values;

namespace UKnack.Concrete.Audio;

public class ToggleToAudioBinding : Dependant
{
    [SerializeField]
    [ValidReference(typeof(Values.SOPrefsAudioVolume))]
    private SOValueMutable<float> valueProvider;

    [SerializeField] private string ToggleName;
    [SerializeField] private UnityEvent onSliderChanged;
    private Toggle _toggle;

    private void Awake()
    {
        if(valueProvider == null)
            throw new ArgumentNullException(nameof(valueProvider));
        if(onSliderChanged == null)
            throw new ArgumentNullException(nameof(onSliderChanged));
        if (string.IsNullOrWhiteSpace(ToggleName))
            throw new Exception("ToggleName should not be null or only whitespaces");
    }

    private void OnValueChanged(ChangeEvent<bool> ev)
    {
        //Debug.Log(JsonUtility.ToJson(valueProvider));
        if (ev.previousValue == ev.newValue)
            return;
        valueProvider.SetValue(ev.newValue ? 1f :0f);
        onSliderChanged.Invoke();
    }

    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
        _toggle = layout.Q<Toggle>(ToggleName);
        if (_toggle == null)
            throw new Exception($"Toggle {ToggleName} not found");

        _toggle.SetValueWithoutNotify(valueProvider.RawValue > 0.00001f);
        _toggle.RegisterCallback<ChangeEvent<bool>>(OnValueChanged);
    }
    protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout) { }
    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        if (_toggle == null)
            throw new Exception($"Toggle {ToggleName} not found");
        _toggle.UnregisterCallback<ChangeEvent<bool>>(OnValueChanged);
        _toggle = null;
    }
}
