using UnityEngine;

namespace Audio
{
    public interface IAudioClip
    {
        float Volume { get; }
        AudioClip Clip { get; }
    }
}