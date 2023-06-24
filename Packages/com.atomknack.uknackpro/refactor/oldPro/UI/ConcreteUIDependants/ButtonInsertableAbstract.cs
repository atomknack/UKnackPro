using System;
using UnityEngine.UIElements;
using UKnack.Attributes;

namespace UKnack.UI;

public abstract class ButtonInsertableAbstract : Dependant
{
    [SerializeField][DisableEditingInPlaymode] private string _idWhereToInsert;
    [SerializeField][ValidReference] private VisualTreeAsset _buttonVisualAsset;
    [SerializeField][DisableEditingInPlaymode] private int _neighbourOrderPreference = 0;

    protected abstract string ButtonText { get; }

    protected VisualElement _localButtonRoot;
    protected Button _button;

    protected abstract void ButtonClicked();

    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
        _localButtonRoot = new VisualElementSortedOnAddition(_neighbourOrderPreference);
        VisualElement instantinatedVisualTreeAssetRoot = _buttonVisualAsset.Instantiate();
//        if(_additionalStyleSheet != null)
//            instantinatedVisualTreeAssetRoot.styleSheets.Add(_additionalStyleSheet);
        _button = instantinatedVisualTreeAssetRoot.Q<Button>();
        _button.text = ButtonText;
        _button.clicked += ButtonClicked;
        _localButtonRoot.TryAddSafeAndOrderCorrectly(instantinatedVisualTreeAssetRoot);

        //if (string.IsNullOrEmpty(_idWhereToInsert))
        //    layout.TryAddSafeAndOrderCorrectly(_localButtonRoot);
        //else 
            if (!layout.Q<VisualElement>(_idWhereToInsert).TryAddSafeAndOrderCorrectly(_localButtonRoot))
                throw new Exception($"Cannot find id '{_idWhereToInsert}' to insert button in layout {layout.name}");

    }

    protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout)
    {
    }

    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        _button.clicked -= ButtonClicked;
        _localButtonRoot.RemoveFromHierarchy();
    }
}