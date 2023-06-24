using System;
using UKnack.Attributes;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.UI
{
    [CreateAssetMenu(fileName = "UIPopUpSimple", menuName = "UKnack/UIWindow/UIPopUpSimple", order = 500)]
    public sealed class UIPopUpSimple : UIPopUpPrefabWindowAbstract
    {

        [SerializeField]
            [ValidReference(nameof(VerifyPrefabStatic))] 
                private GameObject _prefab;
        protected override GameObject GetPrefab() => _prefab;
        protected override void VerifyPrefab(GameObject prefab) =>
            VerifyPrefabStatic(prefab);

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
    }
}