using Gameplay.Services.GameTime;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Services.Audio.Weapon
{
    public class WeaponSound : MonoBehaviour
    {
        [SerializeField] private ExtendedAudioClip _pickingUp;
        [SerializeField] private ExtendedAudioClip _shot;
        [SerializeField] private ExtendedAudioClip _reload;
        
        private IWeapon _owner;
        private IAudioPlayer _audioPlayer;

        private void Awake()
        {
            _owner = GetComponent<IWeapon>();
            if (_owner == null)
            {
                Debug.LogError("Weapon not founded");
            }
        }
        
        [Inject]
        public virtual void Construct(IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        private void Start()
        {
            _owner.ShotDone += OnShotDone;
            _owner.ReloadStarted += OnReloadStarted;
                
            OnPickup();
        }

        private void OnDestroy()
        {
            _owner.ShotDone -= OnShotDone;
            _owner.ReloadStarted -= OnReloadStarted;
        }

        private void OnShotDone()
        {
            _audioPlayer.PlayOneShoot(_shot);
        }
        private void OnReloadStarted(ICooldown cooldown)
        {
            _audioPlayer.PlayOneShoot(_reload);
        }
        private void OnPickup()
        {
            _audioPlayer.PlayOneShoot(_pickingUp);
        }
    }
}