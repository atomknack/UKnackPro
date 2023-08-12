using System;

namespace UKnack.UI.Windows.Aspects;

[System.Obsolete("WIP")]
internal interface IChoiceOk: IAspect
{
    internal Action OkChoiceAction { set; }
    public void OkChoice();
}
