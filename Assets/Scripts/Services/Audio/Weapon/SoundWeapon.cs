using UnityEngine;
using Zenject;

namespace Services.Audio.Weapon
{
    public class SoundWeapon : MonoBehaviour
    {
        [SerializeField] private ExtendedAudioClip _pickingUp;

        private IAudioPlayer _audioPlayer;
        
        [Inject]
        public virtual void Construct(IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }
        
        private void Start()
        {
            _audioPlayer.PlayOneShoot(_pickingUp);
        }
    }
}