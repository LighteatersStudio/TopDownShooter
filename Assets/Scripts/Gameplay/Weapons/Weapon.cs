using UnityEngine;
using Gameplay.Projectiles;
using Zenject;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private float _shotsPerSecond = 2f;
        [SerializeField] private int _bulletAmount = 50;
        [SerializeField] private ParticleSystem _shotEffect;

        private float _shotCooldownTimer;

        private Projectile.Factory _projectileFactory;

        [Inject]
        private void Construct(Projectile.Factory projectileFactory)
        {
            _projectileFactory = projectileFactory;
        }
        
        private void Start()
        {
            RefreshCooldown();
        }

        private void Update()
        {
            _shotCooldownTimer -= Time.deltaTime;
        }

        public bool Shot()
        {
            if (_shotCooldownTimer > 0 || _bulletAmount == 0)
            {
                Debug.Log("False");
                return false;
            }
            
            RefreshCooldown();
            
            --_bulletAmount;

            var launchTransform = transform;
            var projectile = _projectileFactory.Create(launchTransform.position, launchTransform.forward, 1f, TypeDamage.Fire);
            projectile.Launch();
            Debug.Log("Shot");

            return true;
        }

        private void RefreshCooldown()
        {
            _shotCooldownTimer = 1 / _shotsPerSecond;
        }
    }
}