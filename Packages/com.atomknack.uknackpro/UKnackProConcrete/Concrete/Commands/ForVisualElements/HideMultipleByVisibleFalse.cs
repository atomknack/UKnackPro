using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Commands;

namespace UKnack.Concrete.Commands.ForVisualElements
{
    public class HideMultipleByVisibleFalse : CommandMonoBehaviour<VisualElement>
    {
        [SerializeField] private string[] Elements;

        public override void Execute(VisualElement layout)
        {
            foreach (var element in Elements)
            {
                VisualElement found = layout.Q<VisualElement>(element);
                if (found == null)
                    throw new Exception($"VisualElement {found} not found");
                found.visible = false;
            }
        }
    }
}


