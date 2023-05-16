using UnityEngine;

namespace Gameplay.Projectile
{
    public class Projectile : MonoBehaviour
    {
        private const float TimeForDestroyShot = 5;
        
        [SerializeField] private ParticleSystem _sparksEffect;
        
        private float _damage;
        private float _lifeTimer = TimeForDestroyShot;
        private TypeDamage _typeDamage;

        private IProjectileMovement _projectileMovement;

        
        private void Awake()
        {
            _projectileMovement = GetComponent<IProjectileMovement>();
        }

        public void Launch(Vector3 position, Vector3 direction, float damage, TypeDamage typeDamage)
        {
            _projectileMovement.Move(position, direction);
            _damage = damage;
            _typeDamage = typeDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            
            if (!other.GetComponent<Player>())
            {
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
    }
}

