using System;
using UnityEngine.UIElements;
using UKnack.Attributes;
using UKnack.Common;
using UKnack.UI;

namespace UKnack.Preconcrete.UI.Dependants;


public abstract class Dependant : MonoBehaviour, ILayoutDependant
{
    [SerializeField]
    [ProvidedComponent] 
    private LayoutProvider _layoutProvider;
    //internal LayoutProvider GetLayoutProviderEvenIfNotInitialized() => _layoutProvider;
    public void SetDependencyLayoutProvider(LayoutProvider provider)
    {
        if (_layoutProvider != null)
            throw new Exception($"_layoutProvider was already set to {_layoutProvider}, {CommonStatic.BelongingGameobjectName(this)}");
        if (provider == null)
            throw new ArgumentNullException(nameof(provider));
        _layoutProvider = provider;
    }

    private VisualElement _localParent = null;
    public VisualElement LocalParent => _localParent;

    private bool _createdAndNotYetDestroyed;

    protected abstract void OnLayoutCreatedAndReady(VisualElement layout);
    protected virtual void OnLayoutReadyAndAllDependantsCalled(VisualElement layout) { }
    protected abstract void OnLayoutGonnaBeDestroyedNow();

    private protected void Start()
    {
        _createdAndNotYetDestroyed = false;

        _layoutProvider = ProvidedComponentAttribute.Provide(this.gameObject, _layoutProvider);
        /*if (_layoutProvider == null)
        {
            _layoutProvider = GetComponent<LayoutProvider>();
            if (_layoutProvider == null)
                throw new ArgumentException($"Script requires either 1)UILayoutProviderAbstract in same gameObject or 2)have UILayoutProviderAbstract reference setted in UnityEditor (before game starts)");
        }*/
        //Debug.Log($"Start of UIDependantScript {gameObject.name}");
        _layoutProvider.RegisterScript(this);
    }

    private protected void OnDestroy() => DestroyUIDependant();

    void ILayoutDependant.LayoutReady(VisualElement layout) => CreateUIDependant(layout);
    void ILayoutDependant.LayoutReadyAndAllDependantsCalled(VisualElement layout)
    {
        if (_createdAndNotYetDestroyed == false)
            throw new System.InvalidOperationException("layout not created or already destroyed");
        OnLayoutReadyAndAllDependantsCalled(layout);
    }
    void ILayoutDependant.LayoutGonnaBeDestroyedNow() => DestroyUIDependant();
    private void CreateUIDependant(VisualElement layout)
    {
        if (_createdAndNotYetDestroyed == true)
            throw new Exception("this should never happen, like you somehow trying to create UIDependant twice");
        if (_localParent != null)
            throw new InvalidOperationException($"Script already registered to {_localParent} with name {_localParent.name}, so it cannot be registered to {layout} with name {layout.name}");
        _localParent = layout;
        OnLayoutCreatedAndReady(layout);
        _createdAndNotYetDestroyed = true;
    }
    private void DestroyUIDependant()
    {
        if (_createdAndNotYetDestroyed == false)
            return;
        if (_localParent == null)
            throw new ArgumentNullException($"Layout cannot be destroyed because it not registered to somebody");
        OnLayoutGonnaBeDestroyedNow();
        _localParent = null;
        _createdAndNotYetDestroyed = false;
    }
}