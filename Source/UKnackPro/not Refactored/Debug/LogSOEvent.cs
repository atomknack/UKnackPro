using System;
using UnityEngine;
using UKnack;
using UKnack.Events;
using UKnack.Concrete.Commmon;

// Because you can directly call scriptable object from monobehaviour this is probably useless
public class LogSOEvent : MonoBehaviour
{
    [SerializeField] ScriptableLoggerDebugLogOnlyInEditor logger;
    [SerializeField] SOEvent eventToLog;
    [SerializeField] string logTextWhenEvent;

    private void Awake()
    {
        if (logger==null)
            throw new ArgumentNullException(nameof(logger));
        if (eventToLog==null)
            throw new ArgumentNullException(nameof(eventToLog));
    }

    private void LogEvent()=>logger.LogValue(logTextWhenEvent);

    private void OnEnable()
    {
        eventToLog.Subscribe(LogEvent);
    }
    private void OnDisable()
    {
        eventToLog.UnsubscribeNullSafe(LogEvent);
    }
}
