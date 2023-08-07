/*
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
    public abstract class __UIAspectedModalAbstract: ScriptableObjectWithReadOnlyName
    {
        [SerializeField] private bool _isSingleton;
        public abstract int CountOfOpenedWindows();
        public virtual void OpenModal()
        {
            GameObject window;
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

        protected abstract GameObject LastOpenedWindow { get; }
        protected abstract GameObject CreateNewModal();
        protected abstract void InitWindow(GameObject window);
        protected abstract void AfterOpened(GameObject window);
        protected abstract void BeforeClosed(GameObject window);
    }
}
*/