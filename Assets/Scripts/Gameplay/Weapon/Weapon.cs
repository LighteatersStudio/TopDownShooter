using UnityEngine;

namespace Gameplay.Weapon
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        private const float ShotsPerMinuteMultiplier = 60f;
        
        [SerializeField] private Projectile.Projectile _projectilePrefab;
        [SerializeField] private float _shootingSpeed = 120f;
        [SerializeField] private float _playerShootingSpeedModifier = 1f;
        
        private float _shotCooldownTime;
        private float _shotCooldownTimer;
        
        private ParticleSystem _shotEffect;
        private int _bulletAmount = 50;
        
        private void Start()
        {
            _shotCooldownTime = ShotsPerMinuteMultiplier / _shootingSpeed * _playerShootingSpeedModifier;
            _shotCooldownTimer = 0;
        }

        private void Update()
        {
            _shotCooldownTimer -= Time.deltaTime;
        }

        public bool Shot()
        {
            if (_shotCooldownTimer <= 0 && _bulletAmount > 0)
            {
                _shotCooldownTimer = _shotCooldownTime;
                --_bulletAmount;

                var projectile = Instantiate(_projectilePrefab);
                var launchTransform = transform;
                projectile.Launch(launchTransform.position, launchTransform.forward, 1f, TypeDamage.Fire);

                return true;
            }

            return false;
        }
    }
}
