using System;
using UKnack.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UKnack.UI;

[CreateAssetMenu(fileName = "UIDialogBoxFromFileToUnityEvents", menuName = "UKnack/UIWindow/UIDialogBoxFromFileToUnityEvents", order = 700)]
public sealed class UIDialogBoxFromFileToUnityEvents : UIDialogBox
{
    [SerializeField]
        [ValidReference(typeof(UIDialogBox), nameof(UIDialogBox.VerifyPrefabStatic))]
        private GameObject _prefab;
    [SerializeField] private string _header;
    [SerializeField]
        [MarkNullAsColor(0.3f, 0.2f, 0.5f, "if not set, string.Empty will be used")]
            TextAsset _textAsset;
    [SerializeField][FormerlySerializedAs("_onCancel")] private UnityEvent _exitedWithCancel;
    [SerializeField][FormerlySerializedAs("_onOk")] private UnityEvent _exitedWithOk;

    protected override GameObject GetPrefab() => _prefab;
    protected override string GetHeader() => _header;
    protected override string GetLongText() => _textAsset == null ? string.Empty :_textAsset.text;
    protected override void CancelWasSelected() => _exitedWithCancel?.Invoke();
    protected override void OkWasSelected() => _exitedWithOk?.Invoke();
}
