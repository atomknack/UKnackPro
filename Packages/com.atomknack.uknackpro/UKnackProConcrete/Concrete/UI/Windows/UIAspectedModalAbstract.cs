using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Preconcrete.UI.Windows;
using UnityEngine.Events;
using UnityEngine;
using UKnack.UI.Windows;
using UKnack.Common;

namespace UKnack.Preconcrete.UI.Windows
{
    public abstract class UIAspectedModalAbstract: ScriptableObjectWithReadOnlyName
    {
        [SerializeField] private bool _isSingleton;
        public abstract int CountOfOpenedWindows();
        public virtual void OpenModal()
        {
            IValidWindow window;
            if (_isSingleton && CountOfOpenedWindows() > 0)
            {
                window = LastOpenedWindow;
                BeforeClosed(window);
                InitWindow(window);
                AfterOpened(window);
                return;
            }
            
            window = CreateNewModal();
            InitWindow(window);
            AfterOpened(window);
        }

        protected abstract IValidWindow LastOpenedWindow { get; }
        protected abstract IValidWindow CreateNewModal();
        protected abstract void InitWindow(IValidWindow window);
        protected abstract void AfterOpened(IValidWindow window);
        protected abstract void BeforeClosed(IValidWindow window);
    }
}
