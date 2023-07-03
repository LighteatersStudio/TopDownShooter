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

        private Cooldown _shotCooldown;

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
            _shotCooldown = Cooldown.NewFinished();
        }

        private void Start()
        {
            _settings.ViewFactory.Invoke(transform);
            transform.SetParentAndZeroPositionRotation(_user.WeaponRoot);
        }

        private void Update()
        {
            Tick?.Invoke(Time.deltaTime);
        }

        public bool Shot()
        {
            if (!_shotCooldown.IsFinish || !_ammoClip.HasAmmo)
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
            RefillShotCooldown();

            ShotDone?.Invoke();
        }

        private void RefillShotCooldown()
        {
            _shotCooldown = new Cooldown(1 / _settings.ShotsPerSecond * _user.AttackSpeed, this);
        }

        public void Dispose()
        {
            _ammoClip.StopReload();
            Destroy(gameObject);
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
            
            _shotCooldown.ForceFinish();
            var cooldown = _ammoClip.Reload(this);
            
            ReloadStarted?.Invoke(cooldown);
        }

        public class Factory : PlaceholderFactory<IWeaponSettings, IWeaponUser, Weapon>
        {
        }
    }
}