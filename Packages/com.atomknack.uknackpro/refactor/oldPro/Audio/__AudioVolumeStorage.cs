/*
using System;
using UKnack.Events;
using UKnack.Values;
using UKnackBasis.Preconcrete.Values;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UKnack.Audio
{
    [CreateAssetMenu(fileName = "AudioVolumePersistentStorage", menuName = "UKnack/Audio/AudioVolumePersistentStorage", order = 810)]
    public sealed class AudioVolumePersistentStorage : SOValueMutable<float>
    {
        [SerializeField] private USetOrDefault<float> _value;
        public override float RawValue { get => _value.Value; protected set => _value.Value = value; }

        [SerializeField] private AudioVolumePersistentStorage dependUpon;
        [SerializeField][Range(0f,1f)] private float defaultVolume = 1f;
        [SerializeField] private string playerPrefsName;
        [SerializeField] private float _volume;
        private bool _initialized = false;

        private float GetDefaultLocalVolume() => defaultVolume;

        internal override void InternalInvoke(float localVolume)
        {
            SetLocalVolume(localVolume);
        }

        public void SetLocalVolume(float value)
        {
            if (value < 0)
                value = 0;
            _volume = value;
            ParentValueChanged(GetParentValue());
            if (string.IsNullOrEmpty(playerPrefsName) == false)
                PlayerPrefs.SetFloat(playerPrefsName, _volume);
        }

        private void ParentValueChanged(float parentValue)
        {
            //before refactoring: SOEventInternal<float>.InvokeSubscribers(this, parentValue * _volume);
            InvokeSubscribers(this, parentValue * _volume);
        }

        /// <summary>
        /// Same as GetValue, just for convenience
        /// </summary>
        /// <returns></returns>
        public float GetVolume() => GetValue();

        public override float GetValue()
        {
            float localVolume = GetLocalVolume();
            //Debug.Log($"Get value called {GetParentValue()} {_volume} {defaultVolume}");
            return GetParentValue() * localVolume;
        }

        public float GetLocalVolume()
        {
            if (_initialized == false)
                Init();
            return _volume;
        }

        private float GetParentValue()
        {
            if (dependUpon == null)
                return 1f;
            return dependUpon.GetValue();
        }

        protected override void Init()
        {
            if (_initialized)
                return;
            base.Init();

            if (string.IsNullOrEmpty(playerPrefsName))
                _volume = GetDefaultLocalVolume();
            _volume = PlayerPrefs.GetFloat(playerPrefsName, _volume);
 
            if (dependUpon != null)
            {
                dependUpon.Subscribe(ParentValueChanged);
            }
            _initialized = true;
        }
        protected override void Cleanup()
        {
            if (_initialized == false)
                return;
            base.Cleanup();
            if (dependUpon != null)// && dependUpon.IsOneOfSubscribers(ParentValueChanged))
            {
                dependUpon.UnsubscribeNullSafe(ParentValueChanged);
            }

            _initialized = false;
        }
    }
}
*/