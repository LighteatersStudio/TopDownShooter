using System;
using UnityEngine;
using Utility;
using Zenject;

namespace Gameplay
{
    public class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform _viewRoot; 
        [SerializeField] private float _deathWaitTime = 10f; 
        
        private DynamicMonoInitializer<StatsInfo, Func<Transform, GameObject>> _initializer;
            
        private CharacterAnimator _animator;
        private CharacterStats _stats;

        public float HealthRelative => _stats.HealthRelative;
        public bool IsDead => _stats.Health <= 0;


        public event Action<Character> Dead;
        
        [Inject]
        public void Construct(StatsInfo statsInfo, Func<Transform, GameObject> viewFactoryMethod)
        {
            _initializer = new DynamicMonoInitializer<StatsInfo, Func<Transform, GameObject>>(statsInfo, viewFactoryMethod);
        }

        protected void Start()
        {
            _initializer.Initialize(Load);
        }

        private void Load(StatsInfo info, Func<Transform, GameObject> viewFactoryMethod)
        {
            _stats = new CharacterStats(info);
            
            _animator = viewFactoryMethod(_viewRoot).GetComponent<CharacterAnimator>();
            _animator.transform.SetZeroPositionAndRotation();
        }

        public void TakeDamage(float damage)
        {
            if (IsDead)
            {
                return;
            }
            
            _stats.ApplyDamage(damage);
            _animator.PlayHitAnimation();

            if (_stats.Health <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            _animator.PlayDeadAnimation();
            Dead?.Invoke(this);
            
            Destroy(gameObject, _deathWaitTime);
        }
        
        
        public class Factory : PlaceholderFactory<StatsInfo, Func<Transform, GameObject>, Character>
        {
        }
    }
}