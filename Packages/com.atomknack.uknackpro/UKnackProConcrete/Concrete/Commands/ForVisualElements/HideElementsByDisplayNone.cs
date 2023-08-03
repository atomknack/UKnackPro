using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Commands;
using UKnack.Preoncrete.Commands;

namespace UKnack.Concrete.Commands.ForVisualElements
{
    [AddComponentMenu("UKnack/CommandForVisualElements/HideElementsByDisplayNone")]
    public class HideElementsByDisplayNone : ForEachFoundVisualElements
    {
        protected override void DoActionForFoundElement(VisualElement found)
        {
            found.style.display = DisplayStyle.None;
        }
    }
}


