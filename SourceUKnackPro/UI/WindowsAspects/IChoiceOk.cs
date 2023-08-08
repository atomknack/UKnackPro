using System;

namespace UKnack.UI.Windows.Aspects;

[System.Obsolete("WIP")]
internal interface IChoiceOk
{
    internal Action OkChoiceAction { set; }
    public void OkChoice();
}
