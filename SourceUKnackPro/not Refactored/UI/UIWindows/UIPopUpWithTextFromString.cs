using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UKnack.UI;

[CreateAssetMenu(fileName = "UIPopUpWithTextFromString", menuName = "UKnack/UIWindow/UIPopUpWithTextFromString", order = 500)]
public sealed class UIPopUpWithTextFromString : UIPopUpWithText
{
    [SerializeField]
    [ValidReference(typeof(UIPopUpWithText), nameof(UIPopUpWithText.VerifyPrefabStatic))]
    private GameObject _prefab;
    protected override GameObject GetPrefab() => _prefab;

    [SerializeField] private string _header;
    [SerializeField] string _longText;

    protected override string GetHeader() => _header;
    protected override string GetLongText() => _longText;
}
