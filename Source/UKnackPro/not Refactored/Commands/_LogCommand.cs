using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UKnack.Commands;

/// <summary>
/// useless
/// </summary>
[AddComponentMenu($"UKnack/ICommand/{nameof(LogCommand)}")]
[Obsolete("there now is ScriptableLoggerDebugLogOnlyInEditor, so this is probaly useless")]
public sealed class LogCommand : CommandMonoBehaviour
{
    [SerializeField] private string _textToLog;

    public void DebugLog()
    {
        Debug.Log($"{name}: {_textToLog}");
    }
    public void DebugLog(string additionalText)
    {
        Debug.Log($"{name}: {_textToLog};{additionalText}");
    }
    public override void Execute() =>
        DebugLog();
}