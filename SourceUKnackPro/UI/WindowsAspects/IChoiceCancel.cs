using System;

namespace UKnack.UI.Windows.Aspects;

[System.Obsolete("WIP")]
internal interface IChoiceCancel
{
    internal Action CancelChoiceAction { set; }
    public void CancelChoice();
}
