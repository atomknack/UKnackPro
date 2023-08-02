/* replaced by DependantSliderToRawSOValueFloatBinding
 * pending deletion
using System;
using UKnack.Attributes;
using UKnack.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UKnack;
using UKnack.Values;

namespace UKnack.Concrete.Audio;

public class SliderToAudioBinding : Dependant
{
    [SerializeField]
    [ValidReference(typeof(Values.SOPrefsAudioVolume))] 
    private SOValueMutable<float> valueProvider;

    [SerializeField] private string SliderName;
    [SerializeField] private UnityEvent<float> onSliderChanged;
    private Slider _slider;

    private void OnValueChanged(ChangeEvent<float> ev)
    {
        //Debug.Log(JsonUtility.ToJson(valueProvider));
        if (ev.newValue == valueProvider.RawValue)
            return;
        if (ev.previousValue == ev.newValue)
            return;
        valueProvider.SetValue(ev.newValue);
        onSliderChanged.Invoke(ev.newValue);
    }

    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
        _slider = layout.Q<Slider>(SliderName);
        if (_slider == null)
            throw new Exception($"Slider {SliderName} not found");

        _slider.SetValueWithoutNotify(valueProvider.RawValue);
        _slider.RegisterCallback<ChangeEvent<float>>(OnValueChanged);
    }
    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        if (_slider == null)
            throw new Exception($"Slider {SliderName} not found");
        _slider.UnregisterCallback<ChangeEvent<float>>(OnValueChanged);
        _slider = null;
    }

    private void Awake()
    {
        if (valueProvider == null)
            throw new ArgumentNullException(nameof(valueProvider));
        if (onSliderChanged == null)
            throw new ArgumentNullException(nameof(onSliderChanged));
        if (string.IsNullOrWhiteSpace(SliderName))
            throw new Exception("SliderName should not be null or only whitespaces");
    }

}
*/