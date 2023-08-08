using UnityEngine;
using UKnack.Common;
using UKnack.Attributes;
using UKnack.UI.Windows.Aspects;

namespace UKnack.Preconcrete.UI.Windows
{
    internal abstract class SingleModal: ScriptableObjectWithReadOnlyName
    {
        [SerializeField] 
        private string _instantiateUnder = "UIModals";

        [SerializeField]
        [ValidReference(nameof(ValidPrefab))]
        private GameObject _prefab;

        private GameObject _opened;

        protected abstract void ValidateNotNullPrefab(GameObject prefab);

        public virtual void Open()
        {
            if (_opened == null)
                _opened = CreateNewModal();
            
            Init(_opened);
        }

        private GameObject CreateNewModal()
        {
#if UNITY_EDITOR
            ValidPrefab(_prefab);
#endif
            GameObject modal;
            if (string.IsNullOrWhiteSpace(_instantiateUnder))
            {
                return Instantiate(_prefab);
            }

            var root = GameObject.Find("/" + _instantiateUnder);
            if (root == null)
                root = new GameObject(_instantiateUnder);
            return Instantiate(_prefab, root.transform);
            
        }

        protected abstract void Init(GameObject opened);

        protected virtual void CloseModal() 
        {
            if (_opened == null)
                throw new System.InvalidOperationException($"Trying to close Modal, but we have null in {nameof(_opened)})");

            Destroy(_opened);
            _opened = null;
        }

        protected void ValidPrefab(UnityEngine.Object prefab)
        {
            if (prefab == null)
                throw new System.ArgumentNullException(nameof(prefab));
            GameObject go = prefab as GameObject;
            if (go == null)
                throw new System.ArgumentException($"prefab should be GameObject");
            if (go.GetComponent<IModal>() == null)
                throw new System.ArgumentException($"prefab should contain IModal aspect in it root");
            ValidateNotNullPrefab(go);
        }
    }
}
