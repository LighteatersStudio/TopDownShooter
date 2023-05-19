using UnityEngine;
using Zenject;

namespace Gameplay.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private  float _timeForDestroyShot;
        [SerializeField] private ParticleSystem _sparksEffect;
        
        private float _damage;
        private float _lifeTimer;
        private TypeDamage _typeDamage;

        private IProjectileMovement _projectileMovement;

        private Vector3 _position;
        private Vector3 _direction;
        
        private void Awake()
        {
            _projectileMovement = GetComponent<IProjectileMovement>();
        }

        [Inject]
        public void Construct(Vector3 position, Vector3 direction, float damage, TypeDamage typeDamage)
        {
            _position = position;
            _direction = direction;
            _damage = damage;
            _typeDamage = typeDamage;
            _lifeTimer = _timeForDestroyShot;
        }

        public void Launch()
        {
            _projectileMovement.Move(_position, _direction);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<Player>())
            {
                var damageable = other.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(new AttackInfo(_damage, _typeDamage));
                }
                
                SpawnSparksEffect();
                Destroy(gameObject);
            }
        }
        
        private void SpawnSparksEffect()
        {
            var effect = Instantiate(_sparksEffect.gameObject, transform.position, Quaternion.identity);
            Destroy(effect, _sparksEffect.main.startLifetime.constant);
        }
        
        private void Update()
        {
            _lifeTimer -= Time.deltaTime;

            if (_lifeTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        public class Factory : PlaceholderFactory<Vector3, Vector3, float, TypeDamage, Projectile>
        {
        }
    }
}

