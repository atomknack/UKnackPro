using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Attributes;

namespace UKnack.UI
{
    [AddComponentMenu("UKnack/UIProviders/UIDocumentAsProvider")]
    [RequireComponent(typeof(UIDocument))]
    public sealed class UIDocumentAsLayoutProvider : ShouldBeInternalButActuallyPublicClasses.UILayoutProviderDependantRegisterer
    {
        private UIDocument _document;

        [SerializeField] [DisableEditingInPlaymode] private bool rootVisible = true;
        public override float SortingOrder =>
            _document == null ?
            throw new InvalidOperationException($"{nameof(_document)} is null, probably script was not awaken yet") :
            _document.sortingOrder;


        private void OnEnable()
        {
            _document = GetComponent<UIDocument>();
            _root = _document.rootVisualElement;

            if (rootVisible)
                ShouldBeVisible();
            else
                ShouldBeInvisible();
            //Debug.Log(_dependants.Count);
            RemoveNullDependants();
            for (int i = 0; i < _dependants.Count; ++i)
                _dependants[i].LayoutReady(_root);
            for (int i = 0; i < _dependants.Count; ++i)
                _dependants[i].LayoutReadyAndAllDependantsCalled(_root);
            RegisterActive(this);
        }

        private void OnDisable()
        {
            RemoveNullDependants();
            for (int i = 0; i < _dependants.Count; ++i)
                _dependants[i].LayoutGonnaBeDestroyedNow();
            UnRegisterInactive(this);
        }

        public override void ShouldBeVisible()
        {
            rootVisible = true;
            if (isActiveAndEnabled)
                _root.visible = true;
        }

        public override void ShouldBeInvisible()
        {
            rootVisible = false;
            if (isActiveAndEnabled)
                _root.visible = false;
        }
    }
}