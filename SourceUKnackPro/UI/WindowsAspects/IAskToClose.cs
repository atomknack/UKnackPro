using System;

namespace UKnack.UI.Windows.Aspects;

internal interface IAskToClose
{
    internal Action AskToCloseAction { set; }
    public void AskToClose();
}
