using UnityEngine;

namespace Gameplay.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _sparksEffect;
        
        private float _damage;
        private TypeDamage _typeDamage;

        
        public void Launch(float damage, TypeDamage typeDamage)
        {
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
                
                Destroy(gameObject);
                SpawnSparksEffect();
            }
        }
        
        private void SpawnSparksEffect()
        {
            var effect = Instantiate(_sparksEffect.gameObject, transform.position, Quaternion.identity);
            Destroy(effect, _sparksEffect.main.startLifetime.constant);
        }
    }
}

