using System.Collections.Generic;
using System.Linq;
using UKnack.Preconcrete.UI.Windows;
using UKnack.UI.Windows.Aspects;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Concrete.UI.Windows
{
    [AddComponentMenu("UKnack/AspectedModals/SimpleAlertPresenter")]
    internal class SimpleAlertPresenter : AspectedModalSingle
    {
        public virtual void ShowWindow() => OpenModal();

        protected override void ValidateAspects(IAspect[] aspects)
        {
            EnsureThereIsOnlyOneOfEachAspectAndNoUncaccountedFor(
                new System.Type[] { typeof(IModal) },
                TypesOfAspects(aspects)
                );
        }

    }

}

