using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UKnack.Concrete.Audio
{
    [AddComponentMenu("UKnack/Audio/MoveToTransformAndPlayOnEvent")]
    [RequireComponent(typeof(AudioSource))]
    public sealed class AudioMoveToTransformAndPlayOnEvent : AudioPlayOnEvent
    {
        [SerializeField] private Transform moveTo;
        private new void Awake()
        {
            if (moveTo == null)
                throw new ArgumentNullException(nameof(moveTo));
            base.Awake();
        }

        public override void Execute()
        {
            gameObject.transform.position = moveTo.position;
            base.Execute();
        }
    }
}
