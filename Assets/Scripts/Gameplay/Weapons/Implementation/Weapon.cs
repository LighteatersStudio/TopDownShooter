using System;
using UnityEngine;
using Gameplay.Projectiles;
using Gameplay.Services.FX;
using Gameplay.Services.GameTime;
using Zenject;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour, IWeapon, ITicker
    {
        private PlayingFX.Factory _fxFactory;
        private IWeaponUser _user;
        private AmmoClip _ammoClip;

        private IWeaponSettings _settings;

        private float _shotCooldownTimer;

        public string WeaponType => _settings.Id;
        public IHaveAmmo Ammo => _ammoClip;

        public event Action ShotDone;
        public event Action<ICooldown> ReloadStarted;
        public event Action<float> Tick;
        

        [Inject]
        public void Construct(PlayingFX.Factory fxFactory, IWeaponUser user, IWeaponSettings settings)
        {
            _fxFactory = fxFactory;
            _user = user;
            _settings = settings;
            
            _ammoClip = new AmmoClip(_settings.AmmoClipSize, _settings.ReloadTime);
        }

        private void Start()
        {
            _settings.ViewFactory.Invoke(transform);
            transform.SetParentAndZeroPositionRotation(_user.WeaponRoot);
            
            ResetShotCooldown();
        }

        private void Update()
        {
            if (_shotCooldownTimer > 0)
            {
                _shotCooldownTimer -= Time.deltaTime;
            }

            Tick?.Invoke(Time.deltaTime);
        }

        public bool Shot()
        {
            if (_shotCooldownTimer > 0 || !_ammoClip.HasAmmo)
            {
                return false;
            }
            
            ShotInternal();

            if (!_ammoClip.HasAmmo)
            {
                Reload();
            }
            
            return true;
        }

        private void ShotInternal()
        {
            _ammoClip.WasteBullet();
            SpawnProjectile();
            RefreshShotCooldown();

            ShotDone?.Invoke();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void RefreshShotCooldown()
        {
            _shotCooldownTimer = 1 / _settings.ShotsPerSecond * _user.AttackSpeed;
        }

        private void ResetShotCooldown()
        {
            _shotCooldownTimer = 0;
        }
        
        private void SpawnProjectile()
        {
            Vector3 position = transform.position;

            var projectile = Instantiate(_settings.BulletPrefab);

            projectile.Construct(
                new FlyInfo {Position = position, Direction = transform.forward},
                new AttackInfo(_settings.Damage, _settings.TypeDamage), _fxFactory);

            projectile.Launch();

            _fxFactory.Create(_settings.ShotFX, position);
        }
        
        public void Reload()
        {
            if (_ammoClip.Reloading)
            {
                return;
            }
            
            var cooldown = _ammoClip.Reload(this);
            ResetShotCooldown();
            
            ReloadStarted?.Invoke(cooldown);
        }

        public class Factory : PlaceholderFactory<IWeaponSettings, IWeaponUser, Weapon>
        {
        }
    }
}