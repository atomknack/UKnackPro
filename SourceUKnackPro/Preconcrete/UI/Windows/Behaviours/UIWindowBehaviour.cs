using System;
using UKnack.Preconcrete.UI.Dependants;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Preconcrete.UI.Windows
{
    [DisallowMultipleComponent]
    public abstract class UIWindowBehaviour: Dependant
    {
        [NonSerialized] internal bool _pendingDestroy = false;
        [NonSerialized] internal bool _cancelClosing = false;
        public UIWindowScriptableObject windowScriptableObject { get; internal set; }
        public void CloseWindow() => windowScriptableObject.WindowShouldBeClosed(this);

        internal abstract void VerifyWindow();
    }
}
