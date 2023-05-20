using FX;
using UnityEngine;
using Zenject;

namespace Gameplay.Projectiles
{
    [RequireComponent(typeof(IProjectileMovement))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private  float _timeForDestroyShot;
        [SerializeField] private ParticleSystem _sparksEffect;
        
        private IProjectileMovement _projectileMovement;
        private PlayingFX.Factory _fxFactory;

        private FlyInfo _flyInfo;
        private IAttackInfo _attackInfo;
        
        private float _lifeTimer;
        
        
        private void Awake()
        {
            _projectileMovement = GetComponent<IProjectileMovement>();
        }

        [Inject]
        public void Construct(FlyInfo direction, IAttackInfo attackInfo, PlayingFX.Factory fxFactory)
        {
            _flyInfo = direction;
            _attackInfo = attackInfo;
            
            _fxFactory = fxFactory;
        }

        public void Launch()
        {
            transform.parent = null;
            _lifeTimer = _timeForDestroyShot;
            
            _projectileMovement.Move(_flyInfo.Position, _flyInfo.Direction);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<Player>())
            {
                var damageable = other.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(_attackInfo);
                }
                
                SpawnSparksEffect();
                Destroy(gameObject);
            }
        }
        
        private void SpawnSparksEffect()
        {
            var fx = _fxFactory.Create(_sparksEffect, transform.position);
            fx.SetParent(transform);
        }
        
        private void Update()
        {
            _lifeTimer -= Time.deltaTime;

            if (_lifeTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        public class Factory : PlaceholderFactory<FlyInfo, IAttackInfo, Projectile>
        {
        }
    }
}

