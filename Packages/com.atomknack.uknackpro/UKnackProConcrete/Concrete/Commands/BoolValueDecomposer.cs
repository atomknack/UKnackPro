using System;
using System.Collections.Generic;
using System.Text;
using UKnack.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace UKnack.Concrete.Commands
{
    [Obsolete("Not tested")]
    [AddComponentMenu("UKnack/Commands/BoolValueDecomposer")]
    internal class BoolValueDecomposer : CommandMonoBehaviour<bool>
    {
        [SerializeField]
        private UnityEvent<bool> _onTrue;

        [SerializeField]
        private UnityEvent<bool> _onFalse;

        [SerializeField]
        private UnityEvent<bool> _inverted;

        [SerializeField]
        private UnityEvent<bool> _unchanged;

        public override void Execute(bool t)
        {
            if (t)
                _onTrue?.Invoke(t);

            if (!t)
                _onFalse?.Invoke(t);

            _inverted?.Invoke(!t);

            _unchanged?.Invoke(t);
        }
    }
}
