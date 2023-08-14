using UnityEngine;
using UKnack.Common;
using UKnack.Attributes;
using UKnack.UI.Windows.Aspects;
using System.Collections.Generic;
using System.Linq;

namespace UKnack.Preconcrete.UI.Windows
{
    internal abstract class AspectedModalSingle: MonoBehaviour
    {
        [SerializeField] 
        private string _instantiateUnder = "UIModals";

        [SerializeField]
        [ValidReference(nameof(ValidPrefab))]
        private GameObject _aspectedPrefab;

        private GameObject _opened;

//protected abstract void ValidateNotNullPrefab(GameObject prefab);
        protected abstract void ValidateAspects(IAspect[] aspects);

        protected bool IsOpened => _opened == null ? false : true;

        protected virtual void OnDisable()
        {
            if (_opened != null)
            {
                CloseModal();
            }
        }
        protected virtual void Init(GameObject opened)
        {
            opened.SetActive(true);
        }

        protected virtual void OpenModal()
        {
            if (IsOpened == false)
            {
                _opened = CreateNewModal();
                if (IsOpened == false)
                    throw new System.Exception("While trying to open new modal recieved null gameobject");
            }
            TryUpdateCurrentModal();
        }
        protected virtual void TryUpdateCurrentModal()
        {
            if (IsOpened == false)
                return;
            Init(_opened);
        }
        protected virtual void CloseModal()
        {
            if (IsOpened == false)
                Debug.LogError($"Trying to close Modal, but we have null in {nameof(_opened)})");
            Destroy(_opened);
            _opened = null;
        }

        private GameObject CreateNewModal()
        {
            ValidPrefab(_aspectedPrefab);
            if (string.IsNullOrWhiteSpace(_instantiateUnder))
            {
                return Instantiate(_aspectedPrefab);
            }

            var root = GameObject.Find("/" + _instantiateUnder);
            if (root == null)
                root = new GameObject(_instantiateUnder);
            return Instantiate(_aspectedPrefab, root.transform);
            
        }

        protected void ValidPrefab(UnityEngine.Object prefab)
        {
            if (prefab == null)
                throw new System.ArgumentNullException(nameof(prefab));
#if UNITY_EDITOR //heavy lifting we do only in Editor
            GameObject go = prefab as GameObject;
            if (go == null)
                throw new System.ArgumentException($"prefab should be GameObject");
            if (go.GetComponent<IModal>() == null)
                throw new System.ArgumentException($"prefab should contain IModal aspect in it root");
            // if we have enabled gameobject Unity somehow calls it OnEnable before had chance to actually initialize it.
            // why? Don't know. Maybe it because we need to call GetComponent-like methods in initialization, maybe not.
            if (go.activeSelf)
                throw new System.Exception("Modal root should not be enabled Gameobjet");
            //Now we have some kind of modal, more specific tests can be done
//ValidateNotNullPrefab(go);
            IAspect[] aspects = go.GetComponentsInChildren<IAspect>(true);
            ValidateAspects(aspects);
#endif
        }

        protected static TAspect GetAspect<TAspect>(GameObject go)
            => go.GetComponentInChildren<TAspect>(true);


        protected static System.Type[] TypesOfAspects<TAspect>( TAspect[] aspects) => 
            aspects.Select(x => x.GetType()).ToArray();

        protected static void EnsureThereIsOnlyOneOfEachAspectAndNoUncaccountedFor(
            System.Span<System.Type> required, 
            System.Span<System.Type> actual)
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

            static int EnsureOneAndGetItIndex(System.Type a, System.Span<System.Type> aspects)
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

    }
}
