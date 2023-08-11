using UnityEngine;
using UKnack.Common;
using UKnack.Attributes;
using UKnack.UI.Windows.Aspects;

namespace UKnack.Preconcrete.UI.Windows
{
    internal abstract class SingleModal: MonoBehaviour
    {
        [SerializeField] 
        private string _instantiateUnder = "UIModals";

        [SerializeField]
        [ValidReference(nameof(ValidPrefab))]
        private GameObject _prefab;

        private GameObject _opened;

        protected abstract void ValidateNotNullPrefab(GameObject prefab);
        protected virtual void Init(GameObject opened)
        {
            opened.SetActive(true);
        }

        protected virtual void OpenModal()
        {
            if (_opened == null)
            {
                _opened = CreateNewModal();
            }

            Init(_opened);
        }
        protected virtual void CloseModal()
        {
            //if (_opened == null)
            //    throw new System.InvalidOperationException($"Trying to close Modal, but we have null in {nameof(_opened)})");
            Destroy(_opened);
            _opened = null;
        }

        private GameObject CreateNewModal()
        {
            ValidPrefab(_prefab);
            if (string.IsNullOrWhiteSpace(_instantiateUnder))
            {
                return Instantiate(_prefab);
            }

            var root = GameObject.Find("/" + _instantiateUnder);
            if (root == null)
                root = new GameObject(_instantiateUnder);
            return Instantiate(_prefab, root.transform);
            
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
            if (go.activeSelf)
                throw new System.Exception("Modal root should not be enabled Gameobjet");
            //Now we have some kind of modal, more specific tests can be done
            ValidateNotNullPrefab(go);
#endif
        }
    }
}
