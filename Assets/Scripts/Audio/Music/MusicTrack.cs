using UnityEngine;

namespace Audio
{
    public class MusicTrack : IAudioClip
    {
        public AudioClip Clip { get; }
        public float Volume { get; }

        public MusicTrack(AudioClip clip, float volume = 1f)
        {
            Clip = clip;
            Volume = volume;
        }
    }
}