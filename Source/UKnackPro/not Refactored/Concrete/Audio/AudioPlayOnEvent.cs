using System;
using UKnack.Attributes;
using UKnack.Events;
using UKnack.Preconcrete.Commands;

namespace UKnack.Concrete.Audio
{
    [AddComponentMenu("UKnack/Audio/PlayOnEvent")]
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayOnEvent : AbstractCommandSubscribedToSOEvent
    {
        [SerializeField]
        [ValidReference(typeof(IEvent))]
        private UnityEngine.Object _subscribedTo;

        protected override IEvent SubscribedTo => (IEvent)_subscribedTo;

        [SerializeField] private bool _playAudioAsOneShot = false;
        private AudioSource[] _audiosources;

        protected void Awake()
        {
            _audiosources = GetComponents<AudioSource>();
            if (_audiosources == null)
                throw new ArgumentNullException(nameof(_audiosources));
            if (_audiosources.Length == 0)
            {
                throw new Exception("There should be at least one Audiosource on Gameobject for this script to work");
            }
        }
        public override void Execute()
        {
            if (_playAudioAsOneShot)
            {
                foreach (AudioSource source in _audiosources)
                {
                    source.PlayOneShot(source.clip);
                }
                return;
            }

            foreach (AudioSource source in _audiosources)
            {
                source.Play();
            }
        }
    }
}
