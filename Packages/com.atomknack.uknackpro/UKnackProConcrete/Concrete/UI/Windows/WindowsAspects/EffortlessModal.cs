using System;
using System.Collections.Generic;
using System.Text;
using UKnack.UI;
using UKnack.UI.Windows.Aspects;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Concrete.UI.Windows.WindowsAspects
{
    [AddComponentMenu("UKnack/UIWindowAspects/EffortlessModal")]
    [RequireComponent(typeof(UIDocument))]
    internal class EffortlessModal: MonoBehaviour, IModal, IOnScreenOrder
    {
        private UIDocument _document;

        public float SortingOrder => _document.sortingOrder;

        void OnEnable()
        {
            _document = GetComponent<UIDocument>();
            _document.sortingOrder = IOnScreenOrder.NextFreeSpotOnTop();
        }
    }
}
