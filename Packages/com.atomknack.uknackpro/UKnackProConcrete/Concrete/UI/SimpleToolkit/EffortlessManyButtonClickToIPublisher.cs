using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Attributes;

using UKnack.Events;


namespace UKnack.Concrete.UI.SimpleToolkit
{
    [System.Obsolete("Not tested")]
    [AddComponentMenu("UKnack/UI.SimpleToolkit/EffortlessManyButtonClickToIPublisher")]
    internal sealed class EffortlessManyButtonClickToIPublisher : MonoBehaviour
    {
        [SerializeField]
        [ProvidedComponent]
        private UIDocument _document;

        [System.Serializable]
        public struct ItemSource
        {
            public string buttonElementId;

            [ValidReference(typeof(IPublisher), nameof(IPublisher.Validate), typeof(IPublisher))]
            public UnityEngine.Object iPublisher;

        }
        public struct BindingResult
        {
            public UnityEngine.UIElements.Button button;
            public IPublisher iPublisher;
            public System.Action click;
        }

        [SerializeField]
        private ItemSource[] _items;

        private BindingResult[] _bindings;

        private VisualElement _root;

        private void Click(int index)
        {
            _bindings[index].iPublisher.Publish();
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
            catch (System.Exception ex)
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
                _bindings[i].button.clicked -= _bindings[i].click;

                _bindings[i].button = null;
                _bindings[i].iPublisher = null;
                _bindings[i].click = null;
            }
            _bindings = null;
        }

        private void InitBindings()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _bindings[i].button = FindInRoot(_items[i].buttonElementId);
                _bindings[i].iPublisher = IPublisher.Validate(_items[i].iPublisher);

                int indexForEnclosure = i;

                _bindings[i].click = () => Click(indexForEnclosure);
                _bindings[i].button.clicked += _bindings[i].click;
            }
        }
        private Button FindInRoot(string id)
        {
            var ve = _root.Q<Button>(id);
            if (ve == null)
                throw new System.ArgumentNullException($"Button id: {id} not found in UIDocument of {_document.gameObject.name}");
            return ve;
        }

    }
}
