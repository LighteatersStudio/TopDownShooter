using System;
using UnityEngine;
using Gameplay.Projectiles;
using Gameplay.Services.FX;
using Gameplay.Services.GameTime;
using Zenject;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour, IWeapon, ITicker, ICanReload
    {
        private PlayingFX.Factory _fxFactory;
        private IWeaponUser _user;
        private Cooldown.Factory _cooldownFactory;
        private ProjectilePool _projectilePool;
        private IWeaponSettings _settings;
        private Cooldown _shotCooldown;
        private Cooldown _reloadCooldown;

        public string WeaponType => _settings.Id;

        public int RemainAmmo { get; private set; }
        private bool HasAmmo => RemainAmmo > 0;

        public event Action ShotDone;
        public event Action<ICooldown> ReloadStarted;
        public event Action<float> Tick;
        

        [Inject]
        public void Construct(ProjectilePool projectilePool,
            PlayingFX.Factory fxFactory,
            IWeaponUser user,
            IWeaponSettings settings,
            Cooldown.Factory cooldownFactory)
        {
            _fxFactory = fxFactory;
            _user = user;
            _settings = settings;
            _projectilePool = projectilePool;
            _cooldownFactory = cooldownFactory;
            
            _shotCooldown = _cooldownFactory.CreateFinished();
            _reloadCooldown = _cooldownFactory.CreateFinished();
            
            RefillAmmoClip();
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
            if (!_shotCooldown.IsFinish || !HasAmmo)
            {
                return false;
            }
            
            ShotInternal();

            if (!HasAmmo)
            {
                Reload();
            }
            
            return true;
        }

        private void ShotInternal()
        {
            RemainAmmo--;
            SpawnProjectile();
            RefillShotCooldown();

            ShotDone?.Invoke();
        }

        private void RefillShotCooldown()
        {
            _shotCooldown = _cooldownFactory.Create(1 / _settings.ShotsPerSecond * _user.AttackSpeed, this);
            _shotCooldown.Launch();
        }

        public void Dispose()
        {
            _reloadCooldown.ForceFinish();
            Destroy(gameObject);
        }

        private void SpawnProjectile()
        {
            Vector3 position = transform.position;

            _projectilePool.AddProjectile(
                new FlyInfo { Position = position, Direction = transform.forward },
                new AttackInfo(_settings.Damage, _settings.TypeDamage, _user.FriendOrFoeTag));
            
            _fxFactory.Create(_settings.ShotFX, position);
        }
        
        public void Reload()
        {
            if (!_reloadCooldown.IsFinish)
            {
                return;
            }
            
            _shotCooldown.ForceFinish();

            _reloadCooldown = _cooldownFactory.Create(_settings.ReloadTime, this, RefillAmmoClip);
            _reloadCooldown.Launch();

            ReloadStarted?.Invoke(_reloadCooldown);
        }

        private void RefillAmmoClip()
        {
            RemainAmmo = _settings.AmmoClipSize;
        }
        
        public class Factory : PlaceholderFactory<IWeaponSettings, IWeaponUser, Weapon>
        {
        }
    }
}