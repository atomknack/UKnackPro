using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UKnack.Attributes;
using UKnack.UI.ShouldBeInternalButActuallyPublicClasses;

namespace UKnack.UI;

[AddComponentMenu("UKnack/UIProviders/UIInserted")]
public sealed class UIInsertedIdAsLayoutProvider : UILayoutProviderDependantRegisterer, IDependant
{
    [SerializeField]
    //[MarkNullAsColor(0,0.4f,0.2f, "This property will be taken from parent gameobject chain")]
    [ProvidedComponent]
    private UILayoutProviderSorted _parentProvider;
    [SerializeField][DisableEditingInPlaymode] string _idWhereToInsert;
    [SerializeField][ValidReference] private VisualTreeAsset visualTree;
    [SerializeField][DisableEditingInPlaymode] private bool _rootVisibleOnEnable = true;
    [SerializeField][DisableEditingInPlaymode] private bool _invisibleLayoutRetainsSpace = true;
    [SerializeField][DisableEditingInPlaymode] private int _visualElementSortingOrder = 0;

    private bool _rootVisible;

    private VisualElement _IdInParent;
    //private VisualElement _parent;
    //private List<IUIDependant> _dependants = new List<IUIDependant>();

    private bool _scriptWasEnabled = false;
    private bool _parentInformedLayoutCreation = false;

    private bool _subLayoutCreated = false;

    private protected override bool NewDependantCanBeCalled() => _subLayoutCreated;

    public override float SortingOrder => UIParentsWillProvide.ZInverseOrderFromProvider(_parentProvider);

    /* // right now is never called, don't know why
    #if UNITY_EDITOR
        private void OnTransformParentChanged()
        {
            Debug.Log("ontransParChanged");
            if(_parentProvider == null && UIParentsWillProvide.GetProviderFromParent(gameObject) == null)
                Debug.LogError($"{nameof(_parentProvider)} is not set, and no suitable UILayoutProviderAbstract found in gameobject parents.");
        }
    #endif
    */

    private void Awake()
    {
        if (_parentProvider == null)
        {
            _parentProvider= UIParentsWillProvide.GetProviderFromParent(gameObject);
            if (_parentProvider == null)
                throw new ArgumentNullException($"parent provider {nameof(_parentProvider)} is not set and cannot be found in any parent gameobject");
        }
        if (_idWhereToInsert == null)
            throw new ArgumentNullException(nameof(_idWhereToInsert));
        _parentProvider.RegisterScript(this);
    }

    private void CreateLayout()
    {
        if(_parentInformedLayoutCreation == false)
            return;
        if (_scriptWasEnabled == false)
            return;

            CreateSubLayout();

        if (_rootVisible)
            ShouldBeVisible();
        else
            ShouldBeInvisible();
    }

    private void CreateSubLayout()
    {
        Debug.Log($"{gameObject.name} - CreateSubLayout called");
        if (_subLayoutCreated == true)
            return;

            _root = new VisualElementSortedOnAddition(_visualElementSortingOrder);
        _IdInParent.TryAddSafeAndOrderCorrectly(_root);
        _root.Add(visualTree.Instantiate());

        RemoveNullDependants();
        for (int i = 0; i < _dependants.Count; ++i)
            _dependants[i].LayoutReady(_root);

        _subLayoutCreated = true;
    }
    void IDependant.LayoutReadyAndAllDependantsCalled(VisualElement _)
    {
        Debug.Log($"{gameObject.name} - LayoutReadyAndAllDependantsCalled");
        if (_subLayoutCreated == false)
            return;
        Debug.Log($"{gameObject.name} - if this message appear twice with same name, then make another flag like bool _subLayoutCreated");
            //throw new System.InvalidOperationException("Layout cannot tell it delendants that all other called, before creation of sublayout");
        for (int i = 0; i < _dependants.Count; ++i)
            _dependants[i].LayoutReadyAndAllDependantsCalled(_root);
    }


    void ILayoutDependant.LayoutReady(VisualElement layout)
    {
        Debug.Log($"{gameObject.name} - LayoutReady");
        _IdInParent = layout.Q<VisualElement>(_idWhereToInsert);
        if (_IdInParent==null)
            throw new ArgumentNullException($"Parent provider does not contain VisualElement with id {_idWhereToInsert} where this layout should be inserted");

        _parentInformedLayoutCreation = true;

        CreateLayout();
    }

    void DestroySubLayout()
    {
        if (_subLayoutCreated == false)
            return;

        RemoveNullDependants();
        for (int i = 0; i < _dependants.Count; ++i)
            _dependants[i].LayoutGonnaBeDestroyedNow();

        _root.RemoveFromHierarchy();
        _root = null;
        _subLayoutCreated = false;
    }
    void ILayoutDependant.LayoutGonnaBeDestroyedNow()
    {
        _parentInformedLayoutCreation = false;
        DestroySubLayout();
    }

    private void OnEnable()
    {
        _rootVisible = _rootVisibleOnEnable;
        _scriptWasEnabled = true;
        CreateLayout();
        ((IDependant)this).LayoutReadyAndAllDependantsCalled(null);
    }
    private void OnDisable() => ShouldBeInvisible();

    private void OnDestroy() => DestroySubLayout();

    public override void ShouldBeVisible()
    {
        _rootVisible = true;
        if(_scriptWasEnabled && _parentInformedLayoutCreation)
            VisibilitySwitch( true);
    }

    public override void ShouldBeInvisible()
    {
        _rootVisible = false;
        if (_scriptWasEnabled && _parentInformedLayoutCreation)
            VisibilitySwitch(false);
    }

    private void VisibilitySwitch(bool visible)
    {
        if (_invisibleLayoutRetainsSpace)
        {
            VisibleSpaceRetained(_root,visible);
            return;
        }    
        else
        VisibleSpaceHidden(_root,visible);

        void VisibleSpaceRetained(VisualElement element, bool visible) => element.visible = visible;
        void VisibleSpaceHidden(VisualElement element, bool visible)
        {
            if (visible)
                element.style.display = DisplayStyle.Flex;
            else
                element.style.display = DisplayStyle.None;

        }
    }
}