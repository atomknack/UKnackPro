﻿using UnityEngine;
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

//protected abstract void ValidateNotNullPrefab(GameObject prefab);
        protected abstract void ValidateAspects(IAspect[] aspects);

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
            if (_opened == null)
            {
                _opened = CreateNewModal();
                if (_opened == null)
                    throw new System.Exception("While trying to open new modal recieved null gameobject");
            }
            TryUpdateCurrentModal();
        }
        protected virtual void TryUpdateCurrentModal()
        {
            if (_opened == null)
                return;
            Init(_opened);
        }
        protected virtual void CloseModal()
        {
            if (_opened == null)
                Debug.LogError($"Trying to close Modal, but we have null in {nameof(_opened)})");
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

    }
}
