using UnityEngine;

namespace Services.Audio
{
    public interface IAudioClip
    {
        float Volume { get; }
        AudioClip Clip { get; }
    }
}