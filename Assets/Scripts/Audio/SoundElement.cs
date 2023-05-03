using UnityEngine;
using Zenject;

namespace Audio
{
    public class SoundElement<TSound> : MonoBehaviour
    {
        private IAudioPlayer _audioPlayer;
        protected TSound Sounds { get; private set; }

        protected bool IsActive => _audioPlayer != null; 
        

        [Inject]
        public virtual void Construct(IAudioPlayer audioPlayer, TSound uiSoundSettings)
        {
            _audioPlayer = audioPlayer;
            Sounds = uiSoundSettings;
        }

        protected void Play(IAudioClip oneShot)
        {
            if (!IsActive)
            {
                Debug.LogError($"AudioPlayer is null. Can't play sound: {oneShot}");
                return;
            }
            

            _audioPlayer.PlayOneShoot(oneShot);
        }
    }
}