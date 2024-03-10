using System;
using Gameplay.AI;
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
        public void Construct(IFriendFoeSystem friendFoeSystem, PlayingFX.Factory fxFactory, Cooldown.Factory cooldownFactory)
        {
            _friendFoeSystem = friendFoeSystem;
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

            if (IsFriend(friendOrFoeTag))
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
        
        private bool IsFriend(IFriendOrFoeTag friendOrFoeTag)
        {
            return _friendFoeSystem.CheckFriend(_attackInfo.FriendOrFoeTag, friendOrFoeTag);
        }
        private bool IsFoes(IFriendOrFoeTag friendOrFoeTag)
        {
            return _friendFoeSystem.CheckFoes(_attackInfo.FriendOrFoeTag, friendOrFoeTag);
        }
        private void HandleNonTaggedCollision()
        {
            SpawnSparksEffect();
            Dispose();
        }
        
        private bool ShouldIgnoreCollision(Collider other)
        {
            return other.GetComponent<ObserveArea>() || other.GetComponent<Projectile>();
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
            _pool.Despawn(this);
        }
        
        public class Factory : PlaceholderFactory<FlyInfo, IAttackInfo, Projectile>
        {
        }
    }
}

