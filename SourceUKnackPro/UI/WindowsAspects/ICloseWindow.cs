using System;

namespace UKnack.UI.Windows
{
    [System.Obsolete("WIP")]
    internal interface ICloseWindow
    {
        internal Action CloseWindowAction { set; }
        public void CloseWindow();
    }
}
