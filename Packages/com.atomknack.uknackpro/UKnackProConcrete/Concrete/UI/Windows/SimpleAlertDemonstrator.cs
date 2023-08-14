using System.Collections.Generic;
using System.Linq;
using UKnack.Preconcrete.UI.Windows;
using UKnack.UI.Windows.Aspects;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Concrete.UI.Windows
{
    [AddComponentMenu("UKnack/AspectedModals/SimpleAlertDemonstrator")]
    internal class SimpleAlertDemonstrator : AspectedModalSingle
    {
        [SerializeField]
        private UnityEvent _askedToClose;

        public virtual void ShowWindow() => OpenModal();

        private void AskedToClose()
        {
            _askedToClose?.Invoke();
            CloseModal();
        }

        protected override void ValidateAspects(IAspect[] aspects)
        {
            EnsureThereIsOnlyOneOfEachAspectAndNoUncaccountedFor(
                new System.Type[] { typeof(IModal), typeof(IAskToClose)},
                TypesOfAspects(aspects)
                );
        }

        protected override void Init(GameObject opened)
        {
            var askToClose = GetAspect<IAskToClose>(opened);
            askToClose.AskToCloseAction = AskedToClose;
            base.Init(opened);
        }

    }

}

