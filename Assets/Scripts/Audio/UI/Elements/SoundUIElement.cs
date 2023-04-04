using UnityEngine;
using Zenject;

namespace Audio
{
    public class SoundUIElement : MonoBehaviour
    {
        private IAudioPlayer _audioPlayer;
        protected IUISounds Sounds { get; private set; }

        protected bool IsActive => _audioPlayer != null; 
        

        [Inject]
        public virtual void Construct(IAudioPlayer audioPlayer, IUISounds uiSoundSettings)
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