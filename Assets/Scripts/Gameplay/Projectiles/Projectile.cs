using System;
using Gameplay.AI;
using Gameplay.Services.FX;
using Gameplay.Services.GameTime;
using RayFire;
using UnityEngine;
using Zenject;

namespace Gameplay.Projectiles
{
    [RequireComponent(typeof(IProjectileMovement))]
    public class Projectile : MonoBehaviour, ITicker, IPoolable<FlyInfo, IAttackInfo, IMemoryPool>, IDisposable
    {
        private const string Collectable = "Collectable";
        private const string ProjectileIgnore = "ProjectileIgnore";
        
        [SerializeField] private float _timeForDestroyShot;
        [SerializeField] private ParticleSystem _sparksEffect;
        [SerializeField] private RayfireBomb _rayFireBomb;

        private PlayingFX.Factory _fxFactory;
        private Cooldown.Factory _cooldownFactory;
        private FlyInfo _flyInfo;
        private Cooldown _destroyDelay;
        private IFriendFoeSystem _friendFoeSystem;
        private IAttackInfo _attackInfo;
        private IMemoryPool _pool;
        private IProjectileMovement _projectileMovement;
        private IExplosionAction _explosionAction;

        private float _lifeTimer;

        public event Action<float> Tick;

        private void Awake()
        {
            if (!_rayFireBomb)
            {
                Debug.LogWarning("RayFire Bomb is undefined");
            }
            
            _projectileMovement = GetComponent<IProjectileMovement>();
            _explosionAction = GetComponent<IExplosionAction>();
        }

        [Inject]
        public void Construct(IFriendFoeSystem friendFoeSystem, PlayingFX.Factory fxFactory, Cooldown.Factory cooldownFactory)
        {
            _friendFoeSystem = friendFoeSystem;
            _fxFactory = fxFactory;
            _cooldownFactory = cooldownFactory;
        }

        public void Launch()
        {
            transform.parent = null;

            LaunchInternal();
        }

        protected virtual void LaunchInternal()
        {
            _destroyDelay = _cooldownFactory.Create(_timeForDestroyShot, this, DestroyByLifeTime);
            _destroyDelay.Launch();

            _projectileMovement.Move(_flyInfo);
        }

        public void ClearPool()
        {
            _pool.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ShouldIgnoreCollision(other))
            {
                return;
            }

            var friendOrFoeTag = other.GetComponent<IFriendOrFoeTag>();

            if (friendOrFoeTag == null)
            {
                HandleNonTaggedCollision();
                return;
            }

            if (!IsFoes(friendOrFoeTag))
            {
                return;
            }

            HandleNonTaggedCollision();

            var target = other.GetComponent<IDamageable>();

            if (target == null)
            {
                return;
            }

            if (IsFoes(friendOrFoeTag))
            {
                target.TakeDamage(_attackInfo);
            }
        }

        private bool IsFoes(IFriendOrFoeTag friendOrFoeTag)
        {
            return _friendFoeSystem.CheckFoes(_attackInfo.FriendOrFoeTag, friendOrFoeTag);
        }

        protected void HandleNonTaggedCollision()
        {
            ApplyExplosionDamage();
            SpawnSparksEffect();
            Dispose();
        }

        private void ApplyExplosionDamage()
        {
            _explosionAction?.Blast(_friendFoeSystem, _attackInfo);
        }

        private bool ShouldIgnoreCollision(Collider other)
        {
            return other.gameObject.CompareTag(ProjectileIgnore)
                   || other.GetComponent<ObserveArea>()
                   || other.GetComponent<Projectile>()
                   || other.gameObject.CompareTag(Collectable);
        }

        private void SpawnSparksEffect()
        {
            _fxFactory.Create(_sparksEffect, new FXContext(transform.position, -transform.forward));
        }

        private void Update()
        {
            Tick?.Invoke(Time.deltaTime);
        }

        private void DestroyByLifeTime()
        {
            Dispose();
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
            _rayFireBomb?.Explode(0);
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<FlyInfo, IAttackInfo, Projectile>
        {
        }
    }
}

