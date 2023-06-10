using UnityEngine;
using Gameplay.Projectiles;
using Gameplay.Services.FX;
using Zenject;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [Header("Shooting settings")] [SerializeField]
        private string _id;

        [SerializeField] private Projectile _bulletPrefab;
        [SerializeField] private float _shotsPerSecond = 2f;
        [SerializeField] private int _bulletAmount = 50;

        [Header("Damage settings")] [SerializeField]
        private float _weaponDamage = 1f;

        [SerializeField] private TypeDamage _typeDamage = TypeDamage.Fire;

        [Header("FX")] [SerializeField] private ParticleSystem _shotFX;

        private PlayingFX.Factory _fxFactory;
        private IWeaponUser _user;
        private AmmoClip _ammoClip;
        private PoolProjectiles _poolProjectiles;

        private float _shotCooldownTimer;

        public string WeaponType => _id;
        public IHaveAmmo Ammo => _ammoClip;
        

        [Inject]
        public void Construct(PlayingFX.Factory fxFactory, IWeaponUser user, PoolProjectiles poolProjectiles)
        {
            _fxFactory = fxFactory;
            _user = user;
            _poolProjectiles = poolProjectiles;
            
            _ammoClip = new AmmoClip(_bulletAmount);
        }

        private void Start()
        {
            RefreshCooldown();

            transform.SetParentAndZeroPositionRotation(_user.WeaponRoot);
        }

        private void Update()
        {
            if (_shotCooldownTimer > 0)
            {
                _shotCooldownTimer -= Time.deltaTime;
            }
        }

        public bool Shot()
        {
            if (_shotCooldownTimer > 0)
            {
                return false;
            }
            
            if (!_ammoClip.HasAmmo)
            {
                Reload();
            }
            
            RefreshCooldown();
            _ammoClip.WasteBullet();
            SpawnProjectile();
            
            return true;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void RefreshCooldown()
        {
            _shotCooldownTimer = 1 / _shotsPerSecond * _user.AttackSpeed;
        }

        private void SpawnProjectile()
        {
            Vector3 position = transform.position;

            var projectile = Instantiate(_bulletPrefab);

            projectile.Construct(
                new FlyInfo {Position = position, Direction = transform.forward},
                new AttackInfo(_weaponDamage, _typeDamage), _fxFactory, _poolProjectiles);

            projectile.Launch();

            _fxFactory.Create(_shotFX, position);
        }
        
        public void Reload()
        {
            _ammoClip.Reload();
        }
    }
}