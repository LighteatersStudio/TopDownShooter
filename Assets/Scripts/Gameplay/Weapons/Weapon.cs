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
        [Header("Game ID")]
        [SerializeField] private string _id;
        
        [Header("Shooting settings")]
        [SerializeField] private Projectile _bulletPrefab;
        [SerializeField] private float _shotsPerSecond = 2f;
        [SerializeField] private int _bulletAmount = 50;
        [SerializeField] private int _reloadTime = 1;

        [Header("Damage settings")] 
        [SerializeField] private float _weaponDamage = 1f;
        [SerializeField] private TypeDamage _typeDamage = TypeDamage.Fire;

        [Header("FX")] 
        [SerializeField] private ParticleSystem _shotFX;

        private PlayingFX.Factory _fxFactory;
        private IWeaponUser _user;
        private AmmoClip _ammoClip;

        private float _shotCooldownTimer;

        public string WeaponType => _id;
        public IHaveAmmo Ammo => _ammoClip;

        public event Action ShotDone;
        public event Action<ICooldown> ReloadStarted;
        public event Action<float> Tick;
        

        [Inject]
        public void Construct(PlayingFX.Factory fxFactory, IWeaponUser user)
        {
            _fxFactory = fxFactory;
            _user = user;
            
            _ammoClip = new AmmoClip(_bulletAmount, _reloadTime);
        }

        private void Start()
        {
            ResetShotCooldown();
            transform.SetParentAndZeroPositionRotation(_user.WeaponRoot);
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
            _shotCooldownTimer = 1 / _shotsPerSecond * _user.AttackSpeed;
        }

        private void ResetShotCooldown()
        {
            _shotCooldownTimer = 0;
        }
        
        private void SpawnProjectile()
        {
            Vector3 position = transform.position;

            var projectile = Instantiate(_bulletPrefab);

            projectile.Construct(
                new FlyInfo {Position = position, Direction = transform.forward},
                new AttackInfo(_weaponDamage, _typeDamage), _fxFactory);

            projectile.Launch();

            _fxFactory.Create(_shotFX, position);
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
    }
}