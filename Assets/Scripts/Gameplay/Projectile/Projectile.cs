using UnityEngine;

namespace Gameplay.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _sparksEffect;
        
        private float _damage;
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
            if (!other.GetComponent<Player>())
            {
                if (other.GetComponent<IDamageable>() != null)
                {
                    other.GetComponent<IDamageable>().TakeDamage(new AttackInfo(_damage, _typeDamage));
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
    }
}

