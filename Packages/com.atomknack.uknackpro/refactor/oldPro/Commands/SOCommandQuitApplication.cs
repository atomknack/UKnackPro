using System;
using UKnack.Common;
using UnityEngine;

namespace UKnack.Commands;

[CreateAssetMenu(fileName = "SOCommandQuitApplication", menuName = "UKnack/SOCommand/SOCommandQuitApplication", order = 3000)]
internal class SOCommandQuitApplication : ScriptableObjectWithReadOnlyName, ICommand
{
    [NonSerialized] private string _description = nameof(SOCommandQuitApplication);
    public string Description => _description;

    public void Execute()
    {
        Application.Quit();
    }
}
