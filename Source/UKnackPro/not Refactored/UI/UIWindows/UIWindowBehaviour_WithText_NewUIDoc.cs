using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.UI
{
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(UIDocumentAsLayoutProvider))]
    [AddComponentMenu($"UKnack/UIWindowProviders/{nameof(UIWindowBehaviour_WithText_NewUIDoc)}")]
    [DisallowMultipleComponent]
    public class UIWindowBehaviour_WithText_NewUIDoc : UIWindowBehaviour_WithText
    {
        protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout) { }
        protected override void OnLayoutGonnaBeDestroyedNow() { }

        internal override void VerifyWindow()
        {
            UIWindowBehaviour_Simple_NewUIDoc.VerifyWindowPrefab(this);
        }
    }
}
