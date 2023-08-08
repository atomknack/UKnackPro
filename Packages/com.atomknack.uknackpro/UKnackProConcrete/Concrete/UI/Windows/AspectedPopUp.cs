using System;
using UKnack.Preconcrete.UI.Windows;
using UKnack.UI.Windows.Aspects;
using UnityEngine;

namespace UKnack.Concrete.UI.Windows
{
    [CreateAssetMenu(fileName = "AspectedPopUp", menuName = "UKnack/AspectedModals/AspectedPopUp", order = 700)]
    internal class AspectedPopUp : SingleModal
    {
        protected virtual void PopUpAskedToClose()
        {
            CloseModal();
        }

        protected override void Init(GameObject opened)
        {
            var askToClose = GetAspect<IAskToClose>(opened);
            askToClose.AskToCloseAction = PopUpAskedToClose;
        }

        protected override void ValidateNotNullPrefab(GameObject prefab)
        {
            ValidateAspect<IAskToClose>(prefab);
        }

        protected static void ValidateAspect<TAspect>(GameObject prefab)
        {
            if (GetAspect<TAspect>(prefab) == null)
                throw new System.Exception($"prefab should contain {nameof(TAspect)} aspect");
        }
        protected static TAspect GetAspect<TAspect>(GameObject go) 
            => go.GetComponentInChildren<TAspect>(true);
        
    }

}

