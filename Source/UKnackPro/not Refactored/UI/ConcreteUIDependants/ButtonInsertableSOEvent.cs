using System;
using UKnack.Attributes;
using UKnack.Events;

namespace UKnack.UI;

public class ButtonInsertableSOEvent : ButtonInsertableAbstract
{
    [SerializeField][DisableEditingInPlaymode] protected string _buttonText;
    [SerializeField] private SOPublisher _onClicked;

    protected override string ButtonText => _buttonText;

    private void Awake()
    {
        if (_onClicked == null)
            throw new NullReferenceException(nameof(_onClicked));
    }
    protected override void ButtonClicked()
    {
        _onClicked.Publish();
    }
}