using System;
using Gameplay.Services.FX;
using Gameplay.Services.GameTime;
using UnityEngine;
using Zenject;

namespace Gameplay.Projectiles
{
    [RequireComponent(typeof(IProjectileMovement))]
    public class Projectile : MonoBehaviour, ITicker, IPoolable<FlyInfo, IAttackInfo, IMemoryPool>, IDisposable
    {
        [SerializeField] private float _timeForDestroyShot;
        [SerializeField] private ParticleSystem _sparksEffect;

        private PlayingFX.Factory _fxFactory;
        private Cooldown.Factory _cooldownFactory;
        private FlyInfo _flyInfo;
        private ProjectilePool _projectilePool;
        private Cooldown _cooldown;
        private IFriendFoeSystem _friendFoeSystem;
        private IAttackInfo _attackInfo;
        private IMemoryPool _pool;
        private IProjectileMovement _projectileMovement;

        private float _lifeTimer;

        public event Action<float> Tick;

        private void Awake()
        {
            _projectileMovement = GetComponent<IProjectileMovement>();
        }

        [Inject]
        public void Construct(ProjectilePool projectilePool,
            IFriendFoeSystem friendFoeSystem,
            PlayingFX.Factory fxFactory,
            Cooldown.Factory cooldownFactory)
        {
            _friendFoeSystem = friendFoeSystem;
            _fxFactory = fxFactory;
            _cooldownFactory = cooldownFactory;
            _projectilePool = projectilePool;
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
            var target = other.GetComponent<IDamageable>();

            SpawnSparksEffect();

            _projectilePool.RemoveProjectile(this);

            if (target == null)
            {
                return;
            }

            var fiendOrFoeTag = other.GetComponent<IFriendOrFoeTag>();

            if (fiendOrFoeTag == null || _friendFoeSystem.CheckFoes(_attackInfo.FriendOrFoeTag, fiendOrFoeTag))
            {
                target.TakeDamage(_attackInfo);
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
            _projectilePool.RemoveProjectile(this);
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(FlyInfo flyInfo, IAttackInfo attackInfo, IMemoryPool pool)
        {
            _flyInfo = flyInfo;
            _attackInfo = attackInfo;
            _pool = pool;
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<FlyInfo, IAttackInfo, Projectile>
        {
        }
    }
}

