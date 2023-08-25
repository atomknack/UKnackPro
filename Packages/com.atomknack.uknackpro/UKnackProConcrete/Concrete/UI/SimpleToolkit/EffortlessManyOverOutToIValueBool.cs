using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Attributes;
using UKnack.UI;
using UKnack.Concrete.UI.Windows;
using System;
using System.Linq;
using UKnack.Values;
using System.Collections.Generic;
using UKnack.Events;
using UKnack.Preconcrete.UI.SimpleToolkit;
using static UnityEngine.InputSystem.HID.HID;

namespace UKnack.Concrete.UI.SimpleToolkit
{
    [Obsolete("Not tested")]
    [AddComponentMenu("UKnack/UI.SimpleToolkit/EffortlessManyOverOutToIValueBool")]
    internal sealed class EffortlessManyOverOutToIValueBool: MonoBehaviour
    {
        [SerializeField]
        [ProvidedComponent]
        private UIDocument _document;

        [System.Serializable]
        public struct ItemSource
        {
            public string visualElementId;

            [ValidReference(typeof(IPublisher<bool>), nameof(IPublisher<bool>.Validate), typeof(IPublisher<bool>))]
            public UnityEngine.Object iPublisherBool;

            public bool inverse;
        }
        public struct BindingResult
        {
            public VisualElement visualElement;
            public IPublisher<bool> iPublisher;
            public EventCallback<MouseEnterEvent> enter;
            public EventCallback<MouseLeaveEvent> leave;
        }

        [SerializeField]
        private ItemSource[] _items;

        private BindingResult[] _bindings;

        private VisualElement _root;

        private void ElementMouseEnter(int index) =>
            PublishForIndex(index, true);

        private void ElementMouseLeave(int index) =>
            PublishForIndex(index, false);

        private void PublishForIndex(int index, bool value)
        {
            if (_items[index].inverse)
                value = !value;
            _bindings[index].iPublisher.Publish(value);
        }

        private void OnEnable() 
        {
            _document = ProvidedComponentAttribute.Provide<UIDocument>(gameObject, _document);

            _root = _document.rootVisualElement;

            _bindings = new BindingResult[_items.Length];

            try
            {
                InitBindings();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                _bindings = null;
            }
        }

        void OnDisable() 
        {
            if (_bindings == null)
                return;
            for (int i = 0; i < _bindings.Length; i++)
            {
                _bindings[i].visualElement.UnregisterCallback<MouseEnterEvent>(_bindings[i].enter);

                _bindings[i].visualElement.UnregisterCallback<MouseLeaveEvent>(_bindings[i].leave);

                _bindings[i].visualElement = null;
                _bindings[i].iPublisher = null;
                _bindings[i].enter = null;
                _bindings[i].leave = null;
            }
            _bindings = null;
        }

        private void InitBindings()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _bindings[i].visualElement = FindInRoot(_items[i].visualElementId);
                _bindings[i].iPublisher = IPublisher<bool>.Validate(_items[i].iPublisherBool);

                int indexForEnclosure = i;

                _bindings[i].enter = _ => ElementMouseEnter(indexForEnclosure);
                _bindings[i].visualElement.RegisterCallback<MouseEnterEvent>(_bindings[i].enter);

                _bindings[i].leave = _ => ElementMouseLeave(indexForEnclosure);
                _bindings[i].visualElement.RegisterCallback<MouseLeaveEvent>(_bindings[i].leave);

            }
        }
        private VisualElement FindInRoot(string id)
        {
            var ve = _root.Q<VisualElement>(id);
            if (ve == null)
                throw new System.ArgumentNullException($"VisualElementwith id: {id} not found in UIDocument of {_document.gameObject.name}");
            return ve;
        }

    }
}
