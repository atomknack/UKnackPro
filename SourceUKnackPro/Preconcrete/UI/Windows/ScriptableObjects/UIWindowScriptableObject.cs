using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Preconcrete.UI.Windows
{
    public abstract class UIWindowScriptableObject : ScriptableObject
    {
        [SerializeField] private bool _isSingleton;

        [NonSerialized] private UIWindowBehaviour _lastOpened = null;
        public abstract UnityEvent<UIWindowBehaviour> EventAfterOpen { get; }
        public abstract UnityEvent<UIWindowBehaviour> EventBeforeClosed { get; }
        public UIWindowBehaviour LastOpenedWindow { get { return _lastOpened; } }
        public abstract int CountOfOpenedWindows();
        public virtual void OpenNewModal()
        {
            if (_isSingleton && CountOfOpenedWindows() > 0)
                return;
            _lastOpened = ModalWindowCreation();
            EventAfterOpen.Invoke(_lastOpened);
        }
        internal virtual void WindowShouldBeClosed(UIWindowBehaviour window)
        {
            EventBeforeClosed.Invoke(window);
            if (window._cancelClosing == true)
            {
                window._cancelClosing = false;
                return;
            }
            window._pendingDestroy= true;
            Destroy(window.gameObject);
        }

        protected abstract UIWindowBehaviour ModalWindowCreation();
    }
}
