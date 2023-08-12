using System;

namespace UKnack.UI.Windows.Aspects;

internal interface IAskToClose: IAspect
{
    internal Action AskToCloseAction { set; }
    public void AskToClose();
}
