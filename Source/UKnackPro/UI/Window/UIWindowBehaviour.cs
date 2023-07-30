using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.UI
{
    [DisallowMultipleComponent]
    public abstract class UIWindowBehaviour: Dependant
    {
        [NonSerialized] internal bool _pendingDestroy = false;
        [NonSerialized] internal bool _cancelClosing = false;
        public UIWindowAbstract windowScriptableObject { get; internal set; }
        public void CloseWindow() => windowScriptableObject.WindowShouldBeClosed(this);

        internal abstract void VerifyWindow();
    }
}
