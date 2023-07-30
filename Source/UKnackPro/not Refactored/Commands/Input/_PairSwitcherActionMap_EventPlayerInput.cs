/* Should be moved to UCompanion
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UKnack;
using UCompanion;
using UKnack.Commands;

namespace UKnack
{
    [Obsolete("not tested")]
    [AddComponentMenu($"UKnack/Input/{nameof(PairSwitcherActionMap_EventPlayerInput)}:ICommand")]
    public class PairSwitcherActionMap_EventPlayerInput : CommandMonoBehaviour
    {
        [Serializable]
        public struct Pair
        {
            public EventPlayerInput input;
            public string actionMapName;
        }
        [SerializeField] private Pair[] pairs;

        private void OnEnable()
        {
            AssertAllActionMapExistingInEveryPlayerInput();
        }
        public override void Execute() => SwitchMap();

        public void SwitchMap()
        {
            foreach (var pair in pairs)
                pair.input.SwitchCurrentActionMap(pair.actionMapName);
        }

        private void AssertAllActionMapExistingInEveryPlayerInput()
        {
            foreach (var pair in pairs)
            {
                pair.input.actions.FindActionMap(pair.actionMapName, true);
            }
        }
    }
}
*/