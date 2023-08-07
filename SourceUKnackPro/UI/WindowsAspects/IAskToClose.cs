using System;

namespace UKnack.UI.Windows;

[System.Obsolete("WIP")]
internal interface IAskToClose
{
    internal Action AskToCloseAction { set; }
    public void AskToClose();
}
