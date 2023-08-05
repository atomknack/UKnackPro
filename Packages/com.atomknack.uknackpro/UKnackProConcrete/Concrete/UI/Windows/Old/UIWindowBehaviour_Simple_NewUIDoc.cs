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
    [AddComponentMenu("UKnack/UIWindowProviders/UIWindowBehaviour_Simple_NewUIDoc")]
    public class SimpleWindowBehaviour_UIDoc : UIWindowBehaviour
    {
        protected override void OnLayoutCreatedAndReady(VisualElement layout) { }
        protected override void OnLayoutReadyAndAllDependantsCalled(VisualElement layout) { }
        protected override void OnLayoutGonnaBeDestroyedNow() { }

        internal override void VerifyWindow()
        {
            VerifyWindowPrefab(this);
        }
        internal static void VerifyWindowPrefab(UIWindowBehaviour window)
        {
            if (window.GetComponent<UIDocument>() == null)
                throw new Exception($"{nameof(SimpleWindowBehaviour_UIDoc)} requires UIDocument in prefab root");
            if (window.GetComponent<UIDocumentAsLayoutProvider>() == null)
                throw new Exception($"{nameof(SimpleWindowBehaviour_UIDoc)} requires UIDocumentAsUILayoutProvider in prefab root");
        }
    }
}
