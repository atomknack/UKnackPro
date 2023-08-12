using System;
using UKnack.Preconcrete.UI.Windows;
using UKnack.UI.Windows.Aspects;
using UnityEngine;

namespace UKnack.Concrete.UI.Windows
{
    [AddComponentMenu("UKnack/AspectedModals/AspectedPopUp")]
    internal class AspectedPopUp : SingleModal
    {
        public virtual void ShowPopUp() => OpenModal();

        protected override void Init(GameObject opened)
        {
            var askToClose = GetAspect<IAskToClose>(opened);
            askToClose.AskToCloseAction = CloseModal;
            base.Init(opened);
        }

        protected override void ValidateNotNullPrefab(GameObject prefab)
        {
            ValidateAspect<IModal>(prefab);
            ValidateAspect<IAskToClose>(prefab);
        }

        protected static void ValidateAspect<TAspect>(GameObject prefab)
        {
            if (GetAspect<TAspect>(prefab) == null)
                throw new System.Exception($"prefab should contain {typeof(TAspect).Name} aspect. Fullname: {typeof(TAspect).FullName}");

            if (prefab.GetComponentsInChildren<TAspect>(true).Length > 1 )
                throw new System.Exception($"Only one {typeof(TAspect)} allowed per Prefab, but found multiple)");
        }
        protected static TAspect GetAspect<TAspect>(GameObject go)
            => go.GetComponentInChildren<TAspect>(true);
        
    }

}

