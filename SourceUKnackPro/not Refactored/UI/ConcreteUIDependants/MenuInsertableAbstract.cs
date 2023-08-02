using System;
using UKnack.Attributes;
using UKnack.Preconcrete.UI.Dependants;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.UI;

public abstract class MenuInsertableAbstract : Dependant
{
    public struct ButtonClickAction
    {
        public string name;
        public Action click;
        public int order;
    }

    [SerializeField][DisableEditingInPlaymode] private string _idWhereToInsert;
    [SerializeField][ValidReference] private VisualTreeAsset _buttonVisualAsset;

    private ButtonClickAction[] _buttonNames;
    private Button[] _buttons;
    VisualElement _menuPlaceholder;
    protected abstract ButtonClickAction[] GetButtonNames();
    protected override void OnLayoutCreatedAndReady(VisualElement layout)
    {
        if (string.IsNullOrWhiteSpace(_idWhereToInsert))
            throw new Exception($"{nameof(_idWhereToInsert)} should not be empty");
        _menuPlaceholder = layout.Q<VisualElement>(_idWhereToInsert);
        if (_menuPlaceholder == null)
            throw new Exception($"Placeholder with name {_idWhereToInsert} not found");

        _buttonNames = GetButtonNames();
        _buttons = new Button[_buttonNames.Length];

        CreateButtons();
    }


    private void CreateButtons()
    {
        for (int i = 0; i< _buttonNames.Length; i++)
        {
            VisualElement _localButtonRoot = new VisualElementSortedOnAddition(_buttonNames[i].order);
            VisualElement instantinatedVisualTreeAssetRoot = _buttonVisualAsset.Instantiate();

            _buttons[i] = instantinatedVisualTreeAssetRoot.Q<Button>();
            _buttons[i].text = _buttonNames[i].name;
            _localButtonRoot.TryAddSafeAndOrderCorrectly(instantinatedVisualTreeAssetRoot);
            _menuPlaceholder.TryAddSafeAndOrderCorrectly(_localButtonRoot);

            _buttons[i].clicked += _buttonNames[i].click;
        }
    }

    protected override void OnLayoutGonnaBeDestroyedNow()
    {
        for (int i = 0; i < _buttonNames.Length; i++)
            _buttons[i].clicked -= _buttonNames[i].click;
    }
}
