using System;

using UnityEngine.UIElements;
using UKnack.Commands;

namespace UKnack.Preoncrete.Commands
{
    public abstract class ForEachFoundVisualElements : CommandMonoBehaviour<VisualElement>
    {
        [SerializeField] protected string[] _elements;

        protected abstract void DoActionForFoundElement(VisualElement found);

        public override void Execute(VisualElement layout)
        {
            foreach (var element in _elements)
            {
                VisualElement found = layout.Q<VisualElement>(element);
                if (found == null)
                    throw new Exception($"VisualElement {found} not found");
                DoActionForFoundElement(found);
            }
        }
    }
}


