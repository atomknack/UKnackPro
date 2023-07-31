using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UKnack.UI.ShouldBeInternalButActuallyPublicClasses;

namespace UKnack.UI
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class UIPopUpPrefabWindowAbstract : UIWindowAbstract
    {
        [SerializeField] private UnityEvent<UIWindowBehaviour> _afterOpen;
        [SerializeField] private UnityEvent<UIWindowBehaviour> _beforeClosed;
        [SerializeField] private string _instantiateUnder = "UIPopUps";
        [SerializeField] private float _defaultZInvertedOrder = 0f;
        [SerializeField] private bool _onTop = true;

        public override UnityEvent<UIWindowBehaviour> EventAfterOpen => _afterOpen;
        public override UnityEvent<UIWindowBehaviour> EventBeforeClosed => _beforeClosed;

        //[SerializeField]
        //    [ValidatedReference(typeof(UIPopUpPrefabWindow), nameof(ValidatePrefab))] 
        //        private GameObject _prefab;
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
        protected abstract GameObject GetPrefab();
        protected abstract void VerifyPrefab(GameObject prefab);
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
                ZInvertedOrderForNewWindowOnTop(_defaultZInvertedOrder): 
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
    }
}
