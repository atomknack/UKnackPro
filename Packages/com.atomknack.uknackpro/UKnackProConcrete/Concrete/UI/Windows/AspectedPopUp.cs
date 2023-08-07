using System;
using UKnack.Preconcrete.UI.Windows;
using UKnack.UI.Windows;
using UnityEngine;

namespace UKnack.Concrete.UI.Windows
{

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
            if (GetAspect<IAskToClose>(prefab) == null)
                throw new System.Exception($"prefab should contain {nameof(IAskToClose)} aspect");
        }

        private static TAspect GetAspect<TAspect>(GameObject go) 
            => go.GetComponentInChildren<TAspect>(true);
    }

}

