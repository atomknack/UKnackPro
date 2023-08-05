using System;

namespace UKnack.UI.Windows
{
    [System.Obsolete("WIP")]
    internal interface IChoiceOk
    {
        internal Action OkChoiceAction { set; }
        public void OkChoice();
    }
}
