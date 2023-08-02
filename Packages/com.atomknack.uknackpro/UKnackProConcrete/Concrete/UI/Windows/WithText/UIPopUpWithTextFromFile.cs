﻿using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace UKnack.Preconcrete.UI.Windows
{

    [CreateAssetMenu(fileName = "UIPopUpWithTextFromFile", menuName = "UKnack/UIWindow/UIPopUpWithTextFromFile", order = 500)]
    public sealed class UIPopUpWithTextFromFile : UIPopUp_WithText
    {
        [SerializeField]
        [ValidReference(typeof(UIPopUp_WithText), nameof(UIPopUp_WithText.VerifyPrefabStatic))]
        private GameObject _prefab;
        protected override GameObject GetPrefab() => _prefab;

        [SerializeField] private string _header;
        [SerializeField]
        [MarkNullAsColor(0.3f, 0.2f, 0.5f, "if not set, string.Empty will be used")]
        TextAsset _textAsset;

        protected override string GetHeader() => _header;
        protected override string GetLongText() => _textAsset == null ? string.Empty : _textAsset.text;
    }
}