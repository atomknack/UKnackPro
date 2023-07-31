/* Should be moved to UCompanion

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UKnack;
using UCompanion;

namespace UKnack.Commands;

[AddComponentMenu($"UKnack/Input/{nameof(MultipleActionMapSwitcher_EventPlayerInput)}:ICommand")]
public class MultipleActionMapSwitcher_EventPlayerInput : CommandMonoBehaviour
{
    [SerializeField] private EventPlayerInput[] eventPlayerInputs;
    [SerializeField] private string[] actionMapNames;

    private int index = 0;

    private void OnEnable()
    {
        AssertAllActionMapExistingInEveryPlayerInput();
    }
    public override void Execute() => 
        SwitchMap();

    public void SwitchMap()
    {
        index++;
        if (index >= actionMapNames.Length)
            index = 0;
        foreach (var input in eventPlayerInputs)
            input.SwitchCurrentActionMap(actionMapNames[index]);

        Debug.Log(index);
    }

    private void AssertAllActionMapExistingInEveryPlayerInput()
    {
        foreach (var playerInput in eventPlayerInputs)
        {
            var actions = playerInput.actions;
            foreach (var actionMap in actionMapNames)
            {
                //true to throw error if action map not found
                actions.FindActionMap(actionMap, true);
            }
        }
    }
}
*/