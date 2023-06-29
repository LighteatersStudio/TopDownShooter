using UnityEngine;
using Zenject;

namespace Services.Audio
{
    
    public class AudioPlayer : IAudioPlayer, IMusicPlayer
    {
        private readonly SoundSource _soundSource;
        
        [Inject]
        public AudioPlayer(SoundSource soundSource)
        {
            _soundSource = soundSource;
        }

        public void PlayOneShoot(IAudioClip audioClip)
        {
            if (audioClip == null || audioClip.Clip == null)
            {
                Debug.LogWarning("PlayOneShoot: audioClip is null");
                return;
            }
            
            _soundSource.Source.PlayOneShot(audioClip.Clip, audioClip.Volume);
        }

        public void PlayMusic(IAudioClip track)
        {
            _soundSource.Source.clip = track.Clip;
            _soundSource.Source.loop = true;
            _soundSource.Source.volume = track.Volume;
            _soundSource.Source.Play();
        }
        
        public void StopMusic()
        {
            _soundSource.Source.Stop();
        }
    }
}