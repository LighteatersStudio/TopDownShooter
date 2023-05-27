using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(menuName = "LightEaters/Audio/Simple Audio Clip", fileName = "AudioClip", order = 0)]
    public class ExtendedAudioClip : ScriptableObject, IAudioClip
    {
        [SerializeField] private float _volume = 1;
        [SerializeField] private AudioClip _clip;

        public float Volume => Mathf.Clamp01(_volume);
        public AudioClip Clip => _clip;
    }
}