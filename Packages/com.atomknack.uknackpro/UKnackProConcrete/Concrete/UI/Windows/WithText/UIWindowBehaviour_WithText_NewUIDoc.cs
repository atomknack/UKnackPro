using System;
using UKnack.Concrete.UI.Providers;
using UKnack.Preconcrete.UI.Windows;
using UKnack.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Concrete.UI.Windows
{
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(UIDocumentAsLayoutProvider))]
    [AddComponentMenu("UKnack/UIWindowProviders/UIWindowBehaviour_WithText_NewUIDoc")]
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
