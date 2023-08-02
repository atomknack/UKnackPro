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
    [AddComponentMenu("UKnack/UIWindowProviders/UIWindowBehaviour_DialogBox_NewUIDoc")]
    [DisallowMultipleComponent]
    public class UIWindowBehaviour_DialogBox_NewUIDoc : UIWindowBehaviour_DialogBox
    {
        protected override void OnLayoutGonnaBeDestroyedNow()
        { }

        protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout)
        {
        }

        internal override void VerifyWindow()
        {
            UIWindowBehaviour_Simple_NewUIDoc.VerifyWindowPrefab(this);
        }
    }
}
