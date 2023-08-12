using System.Collections.Generic;
using System.Linq;
using UKnack.Preconcrete.UI.Windows;
using UKnack.UI.Windows.Aspects;
using UnityEngine;

namespace UKnack.Concrete.UI.Windows
{
    [AddComponentMenu("UKnack/AspectedModals/AspectedPopUp")]
    internal class AspectedPopUp : SingleModal
    {
        public virtual void ShowPopUp() => OpenModal();

        protected override void ValidateAspects(IAspect[] aspects)
        {
            System.Type[] actualAspectTypes = aspects.Select(x => x.GetType()).ToArray();
            EnsureThereIsOnlyOneOfEachAspectAndNoUncaccountedFor(
                new System.Type[] { typeof(IModal), typeof(IAskToClose)},
                actualAspectTypes
                );
        }

        protected override void Init(GameObject opened)
        {
            var askToClose = GetAspect<IAskToClose>(opened);
            askToClose.AskToCloseAction = CloseModal;
            base.Init(opened);
        }

        protected static void EnsureThereIsOnlyOneOfEachAspectAndNoUncaccountedFor(System.Type[] required, System.Type[] actual)
        {
            var accountedFor = new List<int>();
            foreach (var r in required)
            {
                accountedFor.Add(EnsureOneAndGetItIndex(r, actual));
            }
            for (int i = 0; i < actual.Length; i++)
            {
                if (accountedFor.Contains(i) == false)
                    throw new System.Exception($"There is aspect {actual[i]} that is unaccounted for");
            }

            static int EnsureOneAndGetItIndex( System.Type a, System.Span<System.Type> aspects)
            {

                int count = 0;
                int index = 0;
                for (int i = 0; i < aspects.Length; i++)
                    if (a.IsAssignableFrom(aspects[i]))
                    {
                        count++;
                        index = i;
                    }
                if (count == 0)
                    throw new System.Exception($"No assignable '{a}' aspect found in: {string.Join(", ", aspects.ToArray().AsEnumerable())}");
                if (count > 1)
                    throw new System.Exception($"Found more than one '{a}' aspect in: {string.Join(", ", aspects.ToArray().AsEnumerable())}");
                return index;
            }
        }


        /*
         


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

        */
    }

}

