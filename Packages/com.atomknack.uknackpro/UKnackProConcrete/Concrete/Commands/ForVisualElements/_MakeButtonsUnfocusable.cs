/*     /// WIP, probably unneeded because of SwitchFocusablePropertyForUIElements ScriptableObject
using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Commands;
using System;

namespace UKnack.Concrete.Commands.ForVisualElements
{
    /// <summary>
    /// WIP, probably unneeded because of MakeAllButtonsUnfocusable ScriptableObject
    /// </summary>
    [AddComponentMenu("UKnack/CommandForVisualElements/Buttons/MakeAllUnfocusable")]
    public class MakeButtonsUnfocusable : CommandMonoBehaviour<VisualElement>
    {
        [SerializeField]
        [Header("See tooltip")]
        [Tooltip("Probably you don't need to fill any buttons here - just call static method Make")]
        protected string[] _buttons;

        public static void MakeUnfocusableAllButtons(VisualElement layout)
        {
            if (layout == null)
                return;
            layout.Query<Button>().ForEach(x => x.focusable = false);
        }

        public override void Execute(VisualElement layout)
        {
            foreach (var element in _buttons)
            {
                VisualElement found = layout.Q<Button>(element);
                if (found == null)
                    throw new Exception($"VisualElement {found} not found");
                found.focusable = false;
            }
        }


    }
}


*/