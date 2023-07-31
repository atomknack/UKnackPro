using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UKnack.Commands;


namespace UKnack.Concrete.Input
{
    [AddComponentMenu("UKnack/Input/SwitchActionMap_PlayerInput:ICommand")]
    public class SwitchActionMap_PlayerInput : CommandMonoBehaviour
    {
        [SerializeField]
        private PlayerInput _playerInput;
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

