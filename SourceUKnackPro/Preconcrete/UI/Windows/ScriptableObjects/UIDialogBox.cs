using System;

namespace UKnack.Preconcrete.UI.Windows;

public abstract class UIDialogBox : UIPopUp
{
    protected abstract string GetHeader();
    protected abstract string GetLongText();
    protected abstract void OkWasSelected();
    protected abstract void CancelWasSelected();

    protected override UIWindowBehaviour ModalWindowCreation()
    {
        return UIPopUp_WithText.AssignText(base.ModalWindowCreation(), GetHeader(), GetLongText());
    }

    public virtual void OkSelected(UIWindowBehaviour provider)
    {
        WindowShouldBeClosed(provider);
        OkWasSelected();
    }
    public virtual void CancelSelected(UIWindowBehaviour provider)
    {
        WindowShouldBeClosed(provider);
        CancelWasSelected();
    }

    protected override void VerifyPrefab(GameObject prefab) =>
        VerifyPrefabStatic(prefab);

    public static void VerifyPrefabStatic(GameObject prefab)
    {
        UIPopUp_WithText.VerifyPrefabStatic(prefab);
        if (prefab.GetComponent<UIWindowBehaviour_DialogBox>() == null)
            throw new Exception("Prefab should contain UIWindowBehaviour_UIPopUpDialogBox");
    }
}
