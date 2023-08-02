using System;
using UnityEngine;

namespace UKnack.Preconcrete.UI.Windows;

public abstract class UIWindowBehaviour_DialogBox :UIWindowBehaviour_WithText
{
    private UIDialogBox _uiDialogBox;

    public void OkSelected() => _uiDialogBox.OkSelected(this);
    public void CancelSelected() => _uiDialogBox.CancelSelected(this);
    protected new void Start()
    {
        base.Start();
        if (windowScriptableObject is UIDialogBox uiDialogBox)
        {
            _uiDialogBox= uiDialogBox;
            return;
        }
        throw new Exception($"{nameof(UIWindowBehaviour_DialogBox)} has no associated scriptable object of type {nameof(UIDialogBox)}");
    }
}
