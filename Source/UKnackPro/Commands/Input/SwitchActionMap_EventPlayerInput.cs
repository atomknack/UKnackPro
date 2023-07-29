using System;
using UCompanion;
using UKnack.Commands;

namespace UKnack
{
    [AddComponentMenu($"UKnack/Input/{nameof(SwitchActionMap_EventPlayerInput)}:ICommand")]
    public class SwitchActionMap_EventPlayerInput : CommandMonoBehaviour
    {
    [SerializeField]
    private EventPlayerInput _playerInput;
    [SerializeField] 
    private string _actionMapName;

    public override void Execute() => Switch();

    public void Switch()
    {
        _playerInput.SwitchCurrentActionMap(_actionMapName);
    }

    private void OnEnable()
    {
        CheckFields();
        _playerInput.actions.FindActionMap(_actionMapName, true);
    }

    private void CheckFields()
    {
        if (string.IsNullOrEmpty(_actionMapName))
            throw new ArgumentException("{nameof(_actionMapName))} value {_actionMapName}");
        if (_playerInput == null)
            throw new ArgumentNullException(nameof(_playerInput));
    }
}
}
