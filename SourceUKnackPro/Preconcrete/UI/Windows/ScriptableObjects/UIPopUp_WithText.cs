using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UKnack.Preconcrete.UI.Windows;

public abstract class UIPopUp_WithText : UIPopUp
{
    protected abstract string GetHeader();
    protected abstract string GetLongText();

    protected override UIWindowBehaviour ModalWindowCreation()
    {
        return AssignText(base.ModalWindowCreation(), GetHeader(), GetLongText());
    }
    internal static UIWindowBehaviour AssignText(UIWindowBehaviour created, string header, string longText)
    {
        UIWindowBehaviour_WithText popUp = (UIWindowBehaviour_WithText)created;
        popUp._header = header;
        popUp._longText = longText;
        return popUp;
    }
    protected override void VerifyPrefab(GameObject prefab) =>
        VerifyPrefabStatic(prefab);

    public static new void VerifyPrefabStatic(GameObject prefab)
    {
        UIPopUp.VerifyPrefabStatic(prefab);
        if (prefab.GetComponent<UIWindowBehaviour_WithText>() == null)
            throw new Exception($"Prefab should contain {nameof(UIWindowBehaviour_WithText)}");
    }
}
