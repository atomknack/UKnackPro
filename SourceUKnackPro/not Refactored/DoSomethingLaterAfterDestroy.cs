using System;
using System.Threading;
using UKnack.Common;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack
{
    [AddComponentMenu("UKnack/DoSomethingLaterAfterDestroy")]
    [DefaultExecutionOrder(100501)]
    public class DoSomethingLaterAfterDestroy : MonoBehaviour
    {
        [SerializeField][Range(100, 100000)] int waitForMilliseconds = 1000;
        [SerializeField] private UnityEvent whatToDoLater;
        [SerializeField] private bool _dontDestroyOnLoad = true;

        private void Start()
        {

            if (waitForMilliseconds < 100 || waitForMilliseconds > 100000)
                throw new ArgumentException($"time {waitForMilliseconds} should be in Range)");
            if (whatToDoLater == null)
                throw new ArgumentNullException(nameof(whatToDoLater));
            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(this.gameObject);
        }
        private void OnDestroy()
        {
            CommonStatic.DoActionLater(waitForMilliseconds, () => whatToDoLater.Invoke());
        }

    }
}