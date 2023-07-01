using Gameplay.Services.GameTime;
using Services.Audio;
using UnityEngine;
using Zenject;

namespace Gameplay.Weapons
{
    public class WeaponSound : MonoBehaviour
    {
        private IWeapon _owner;
        private IAudioPlayer _audioPlayer;
        private IWeaponSoundSet _settings;

        [Inject]
        public virtual void Construct(IWeapon owner, IAudioPlayer audioPlayer, IWeaponSoundSet settings)
        {
            _owner = owner;
            _audioPlayer = audioPlayer;
            _settings = settings;
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
            _audioPlayer.PlayOneShoot(_settings.Shot);
        }
        private void OnReloadStarted(ICooldown cooldown)
        {
            _audioPlayer.PlayOneShoot(_settings.Reload);
        }
        private void OnPickup()
        {
            _audioPlayer.PlayOneShoot(_settings.PickingUp);
        }
    }
}