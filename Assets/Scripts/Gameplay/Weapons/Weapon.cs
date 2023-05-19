using UnityEngine;
using Gameplay.Projectiles;
using Zenject;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private float _shotsPerSecond = 2f;
        [SerializeField] private int _bulletAmount = 50;
        [SerializeField] private float _weaponDamage = 1f;
        [SerializeField] private TypeDamage _typeDamage = TypeDamage.Fire;
        [SerializeField] private ParticleSystem _shotEffectPrefab;

        private float _shotCooldownTimer;
        private Transform _firingPoint;

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

        public void SetParent(Transform parent)
        {
            _firingPoint = parent;
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

            var projectile = _projectileFactory.Create(_firingPoint.position, _firingPoint.forward, _weaponDamage, _typeDamage);
            projectile.Launch();
            Debug.Log("Shot");

            var shotFX = Instantiate(_shotEffectPrefab, _firingPoint);
            shotFX.Play();
            Destroy(shotFX, 0.5f);

            return true;
        }

        private void RefreshCooldown()
        {
            _shotCooldownTimer = 1 / _shotsPerSecond;
        }
    }
}