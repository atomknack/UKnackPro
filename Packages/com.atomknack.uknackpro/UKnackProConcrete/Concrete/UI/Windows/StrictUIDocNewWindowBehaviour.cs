using System;
using System.Linq;
using UKnack.Attributes;
using UKnack.Concrete.UI.Providers;
using UKnack.Preconcrete.UI.Windows;
using UKnack.UI;
using UKnack.UI.Windows;
using UnityEngine;
using UnityEngine.UIElements;

namespace UKnack.Concrete.UI.Windows
{

    [RequireComponent(typeof(UIDocument))]
    //[AddComponentMenu("UKnack/UIWindowProviders/StrictUIDocNewWindowBehaviour")]
    public abstract class StrictUIDocNewWindowBehaviour : MonoBehaviour, IValidWindow, ISetSortingOrder
    {
        private UIDocument _document;

        private Type[] _allowedAspects;

        protected virtual void ValidateAdditionalRequirements(Span<Type> aspects) 
        {
            // WIP
            // 1 find if all aspects are actually aspects
            // 2 find if gameobject contains no aspects that not in aspects span
            throw new NotImplementedException();
        }

        void ISetSortingOrder.SetSortingOrder(float sortingOrder)
        {
            throw new NotImplementedException();
        }

        void IValidWindow.ValidateRequirements(Span<Type> aspects)
        {
//#if UNITY_EDITOR
            if (GetDocument() == null)
                throw new System.ArgumentNullException($"{nameof(StrictUIDocNewWindowBehaviour)} requires UIDocument");
            if (Absent(aspects, typeof(ISetSortingOrder)))
                throw new System.Exception($"To use {nameof(StrictUIDocNewWindowBehaviour)} you must require {nameof(ISetSortingOrder)}");
            foreach (var aspect in aspects)
                if (GetComponentInChildren(aspect, true) == null)
                    throw new System.Exception($"Window cannot find required aspect: {aspect}");

            ValidateAdditionalRequirements(aspects);
//#endif
        }

        private void Awake()
        {
            _document = GetDocument();
        }

        public TAspect GetAspect<TAspect>()
        {
            if (_allowedAspects == null)
                throw new System.Exception($"Window is not initialized");
            return GetAspect<TAspect>(_allowedAspects);
        }

        private UIDocument GetDocument()=> GetComponent<UIDocument>();

        public void InitWindow(Span<Type> aspects)
        {
            ((IValidWindow)this).ValidateRequirements(aspects);

            if (_allowedAspects == null || _allowedAspects.Length != aspects.Length)
                _allowedAspects = new Type[aspects.Length];
            aspects.CopyTo(_allowedAspects);
        }

        public TAspect GetAspect<TAspect>(Span<Type> allowedAspects)
        {
            if (Absent(allowedAspects, typeof(TAspect)))
                throw new System.Exception($"You trying to get aspect {nameof(TAspect)} that was not specified in window initialization: {String.Join(", ", allowedAspects.ToArray().Select(s => s.ToString()))}");

#if UNITY_EDITOR
            TAspect aspect = (TAspect)Convert.ChangeType(GetAspectComponent(typeof(TAspect)), typeof(TAspect));
#else
            TAspect aspect = GetComponentInChildren<TAspect>(true);
#endif
            if (aspect == null)
                throw new System.Exception($"{nameof(TAspect)} not found in window gameobject: {gameObject.name}");

            return aspect;
        }

        private static bool Absent(Span<Type> where, Type what)
        {
            foreach (var item in where)
                if (item == what)
                    return false;
            return true;
        }
    }
}
