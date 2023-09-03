using System;
using Gameplay.Services.FX;
using Gameplay.Services.GameTime;
using UnityEngine;
using Zenject;

namespace Gameplay.Projectiles
{
    [RequireComponent(typeof(IProjectileMovement))]
    public class Projectile : MonoBehaviour, ITicker
    {
        [SerializeField] private  float _timeForDestroyShot;
        [SerializeField] private ParticleSystem _sparksEffect;
        
        private IProjectileMovement _projectileMovement;
        private PlayingFX.Factory _fxFactory;
        private Cooldown.Factory _cooldownFactory;

        private FlyInfo _flyInfo;
        private IAttackInfo _attackInfo;
        
        private float _lifeTimer;
        private Cooldown _cooldown;
        
        public event Action<float> Tick;
        
        private void Awake()
        {
            _projectileMovement = GetComponent<IProjectileMovement>();
        }

        [Inject]
        public void Construct(FlyInfo direction, IAttackInfo attackInfo, PlayingFX.Factory fxFactory, Cooldown.Factory cooldownFactory)
        {
            _flyInfo = direction;
            _attackInfo = attackInfo;
            _fxFactory = fxFactory;
            _cooldownFactory = cooldownFactory;
        }

        public void Launch()
        {
            transform.parent = null;
            
            _cooldown = _cooldownFactory.Create(_timeForDestroyShot, this, DestroyByLifeTime);
            _cooldown.Launch();
            
            _projectileMovement.Move(_flyInfo);
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
            _fxFactory.Create(_sparksEffect, transform.position);
        }

        private void Update()
        {
            Tick?.Invoke(Time.deltaTime);
        }
        
        private void DestroyByLifeTime()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<FlyInfo, IAttackInfo, Projectile>
        {
        }
    }
}

