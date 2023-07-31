using System;
using System.Threading;
using UKnack.Common;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Concrete.Common
{
    [AddComponentMenu("UKnack/Common/DoSomethingInNewThreadLaterAfterDestroy")]
    [DefaultExecutionOrder(100501)]
    public class DoSomethingInNewThreadLaterAfterDestroy : MonoBehaviour
    {
        [SerializeField][Range(100, 100000)] int waitForMilliseconds = 1000;
        [SerializeField] private UnityEvent whatToDoLater;

        private void Start()
        {

            if (waitForMilliseconds < 100 || waitForMilliseconds > 100000)
                throw new ArgumentException($"time {waitForMilliseconds} should be in Range)");
            if (whatToDoLater == null)
                throw new ArgumentNullException(nameof(whatToDoLater));
        }
        private void OnDestroy()
        {
            CommonStatic.DoActionLaterInNewThread(waitForMilliseconds, () => whatToDoLater.Invoke());
        }

    }
}