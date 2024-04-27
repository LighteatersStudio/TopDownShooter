using System;
using Gameplay.Services.GameTime;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay.CollectableItems
{
    public class WeaponCollectable : MonoBehaviour, ITicker
    {
        public event Action<float> Tick;

        private WeaponSettings _weapon;
        private ILevelWeaponSettings _levelWeaponSettings;
        private Cooldown.Factory _cooldownFactory;
        private Cooldown _destroyCooldown;

        [Inject]
        public void Construct(Vector3 newPosition,
            ILevelWeaponSettings levelWeaponSettings,
            Cooldown.Factory cooldownFactory)
        {
            transform.position = newPosition;
            _levelWeaponSettings = levelWeaponSettings;
            _cooldownFactory = cooldownFactory;

            _destroyCooldown = _cooldownFactory.CreateFinished();
            _weapon = _levelWeaponSettings.WeaponSetting();
        }

        private void Start()
        {
            _weapon.ViewFactory(transform);
            StartDestroyTimer();
        }

        private void Update()
        {
            Tick?.Invoke(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<IPlayer>();

            if ( player != null)
            {
                player.ChangeWeapon(_weapon);
                SelfDestroy();
            }
        }

        private void StartDestroyTimer()
        {
            _destroyCooldown = _cooldownFactory.Create(_levelWeaponSettings.LifeTime, this, SelfDestroy);
            _destroyCooldown.Launch();
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<Vector3, WeaponCollectable>
        {
        }
    }
}