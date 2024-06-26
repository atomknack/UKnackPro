﻿using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Attributes;
using UKnack.UI;
using UKnack.Concrete.UI.Windows;
using System;
using System.Linq;

namespace UKnack.Concrete.UI.SimpleToolkit
{
    [AddComponentMenu("UKnack/UI.SimpleToolkit/InsertManyButtonsOfTextDemonstrator")]
    internal class EffortlessInsertManyButtonsOfTextDemonstrator: ParametrizedTextDemonstrator
    {
        [System.Serializable]
        public struct DemonstratorItem
        {
            public int buttonOrder;
            public string buttonName;
            public string header;
            public TextAsset text;
        }

        [System.Serializable]
        public struct InsertedButton
        {
            public VisualElement element;
            public System.Action click;
        }

        [SerializeField]
        [ProvidedComponent]
        private UIDocument _document;

        [SerializeField]
        [DisableEditingInPlaymode] 
        private string _idWhereToInsert;

        private VisualElement _whereToInsert;
        
        [SerializeField]
        [ValidReference(nameof(ButtonAssetValidation))] 
        private VisualTreeAsset _buttonVisualAsset;

        [SerializeField]
        private DemonstratorItem[] _items;
        
        private InsertedButton[] _insertedButtons;

        private int _lastClickedButton;
        private void ButtonClicked(int index)
        {
            //Debug.Log($"Button {index} clicked");
            ShowDemonstratorModal(_items[index].header, _items[index].text.text);
            _lastClickedButton = index;
        }

        public void ItemsUpdated()
        {
            if (_insertedButtons == null)
                return; //not initialized yet, no need to update any

            if (_items.Length != _insertedButtons.Length)
                throw new System.InvalidOperationException($"Updated items length {_items.Length} differs from {_insertedButtons.Length}");
            for (int i = 0; i < _insertedButtons.Length; i++)
            {
                _insertedButtons[i].element.Q<Button>().text = _items[i].buttonName;
            }
            TryUpdateDemonstratorModal(_items[_lastClickedButton].header, _items[_lastClickedButton].text.text);
        }

        private void OnEnable() 
        {
            _document = ProvidedComponentAttribute.Provide<UIDocument>(gameObject, _document);

            _whereToInsert = _document.rootVisualElement.GetOrThrow<VisualElement>(_idWhereToInsert);

            _insertedButtons = new InsertedButton[_items.Length];
            for (int i = 0; i < _items.Length; i++)
            {
                int index = i;
                InsertedButton inserted = new InsertedButton();
                inserted.element = new VisualElementSortedOnAddition(_items[index].buttonOrder);
                inserted.element.Add(_buttonVisualAsset.Instantiate());
                inserted.click = ()=>ButtonClicked(index);
                inserted.element.Q<Button>().clicked += inserted.click;
                _insertedButtons[index] = inserted;

                _whereToInsert.TryAddSafeAndOrderCorrectly(inserted.element);

                // if something don't work see ButtonInsertableAbstract as example
            }
            ItemsUpdated();
        }

        protected override void OnDisable() 
        {
            for (int i = 0;i < _items.Length;i++)
            {
                _whereToInsert.Remove(_insertedButtons[i].element);
            }
            base.OnDisable();
        }

        private static void ButtonAssetValidation(UnityEngine.Object obj)
        {
            if (obj == null)
                throw new System.ArgumentNullException(nameof(obj));
            VisualTreeAsset asset = obj as VisualTreeAsset;
            if (asset == null)
                throw new System.Exception(nameof(asset));
            var temp = asset.Instantiate();
            Button button = temp.Q<Button>();
            if (button == null)
                throw new System.Exception("button template should contain Button visual element");
            if (temp.Query<Button>().Build().Count() > 1)
                throw new System.Exception("button template should contain only one Button");
        }
    }
}
