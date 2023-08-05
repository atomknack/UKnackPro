using System;
using UKnack.Attributes;
using UKnack.Common;
using UKnack.Preconcrete.UI.Providers;
using UKnack.Preconcrete.UI.Windows;
using UKnack.UI.Windows;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UKnack.Concrete.UI.Windows
{
    public class UIAspectedPopUp : UIAspectedModalAbstract
    {
        [SerializeField] private UnityEvent<IValidWindow> _afterOpen;
        [SerializeField] private UnityEvent<IValidWindow> _beforeClosed;
        [SerializeField] private string _instantiateUnder = "UIPopUps";
        [SerializeField] private float _defaultZInvertedOrder = 0f;
        [SerializeField] private bool _onTop = true;

        protected override IValidWindow LastOpenedWindow => 
            throw new NotImplementedException();

        protected override IValidWindow CreateNewModal()
        {
            throw new NotImplementedException();
        }

        protected override void InitWindow(IValidWindow window)
        {
            ISetSortingOrder order = window.GetAspect<ISetSortingOrder>();
            order.SetSortingOrder(ZInvertedOrderForNewWindowOnTop(_defaultZInvertedOrder));
            InitPopUp(window);
        }

        protected override void AfterOpened(IValidWindow window) =>
            _afterOpen?.Invoke(window);

        protected override void BeforeClosed(IValidWindow window) =>
            _beforeClosed?.Invoke(window);

        public override int CountOfOpenedWindows()
        {
            var existing = FindObjectsOfType<UIWindowBehaviour>(true);
            int count = 0;
            foreach (var found in existing)
                if (this == found.windowScriptableObject && found._pendingDestroy == false)
                    count++;

            //Debug.Log(count);
            return count;
        }
        protected virtual void VerifyPrefab(GameObject prefab) =>
            VerifyPrefabStatic(prefab);

        protected override UIWindowBehaviour ModalWindowCreation()
        {
            var prefab = GetPrefab();
            VerifyPrefab(prefab);

            GameObject newWindow;
            if (string.IsNullOrWhiteSpace(_instantiateUnder))
            {
                newWindow = Instantiate(prefab);
            }
            else
            {
                var root = GameObject.Find("/" + _instantiateUnder);
                if (root == null)
                    root = new GameObject(_instantiateUnder);
                newWindow = Instantiate(prefab, root.transform);
            };
            var uiDocument = newWindow.GetComponent<UIDocument>();
            uiDocument.sortingOrder = _onTop ?
                ZInvertedOrderForNewWindowOnTop(_defaultZInvertedOrder) :
                _defaultZInvertedOrder;
            var provider = newWindow.GetComponent<UIWindowBehaviour>();
            provider.windowScriptableObject = this;
            return provider;


        }

        public static float ZInvertedOrderForNewWindowOnTop(float defaultOrder)
        {
            return Math.Max(defaultOrder, UILayoutProviderSorted.NextFreeSpotOnTop());
            //const float stepCloserToCamera = 100f;
            //if (UILayoutProviderAbstract.TryGetMaxZInversedOrder(out float order))
            //    return order + stepCloserToCamera;
            //return defaultOrder;
        }

        internal static void VerifyPrefabStatic(GameObject prefab)
        {
            if (prefab == null)
                throw new ArgumentNullException($"Prefab should not be null");
            var window = prefab.GetComponent<UIWindowBehaviour>();
            if (window == null)
                throw new ArgumentException($"Prefab root should contain {nameof(UIWindowBehaviour)}");
            window.VerifyWindow();
            //if (prefab.GetComponent<UIDocument>() == null)
            //    throw new ArgumentException($"Prefab root should contain {nameof(UIDocument)}");
        }

        protected abstract void InitPopUp(IValidWindow window);

    }
}