using System;
using Gameplay.View;
using UnityEngine;
using Utility;
using Zenject;

namespace Gameplay
{
    public class Character : MonoBehaviour, IDamageable, IHaveHealth
    {
        [SerializeField] private Transform _viewRoot; 
        [SerializeField] private Transform _healthBarRoot; 
        [SerializeField] private float _deathWaitTime = 10f; 
        
        private DynamicMonoInitializer<StatsInfo, Func<Transform, GameObject>, HealthBar.Factory> _initializer;
            
        private CharacterAnimator _animator;
        private CharacterStats _stats;

        public float HealthRelative => _stats.HealthRelative;
        public bool IsDead => _stats.Health <= 0;


        public event Action HealthChanged
        {
            add => _stats.HealthChanged += value;
            remove => _stats.HealthChanged -= value;
        }
        public event Action<Character> Dead;
        
        [Inject]
        public void Construct(StatsInfo statsInfo, Func<Transform, GameObject> viewFactoryMethod, HealthBar.Factory healthBarFactory)
        {
            _initializer = new DynamicMonoInitializer<StatsInfo, Func<Transform, GameObject>, HealthBar.Factory>(
                statsInfo,
                viewFactoryMethod,
                healthBarFactory);
        }

        protected void Start()
        {
            _initializer.Initialize(Load);
        }

        private void Load(StatsInfo info, Func<Transform, GameObject> viewFactoryMethod, HealthBar.Factory healthBarFactory)
        {
            _stats = new CharacterStats(info);

            _animator = viewFactoryMethod(_viewRoot).GetComponent<CharacterAnimator>();
            _animator.transform.SetZeroPositionAndRotation();
            
            healthBarFactory.Create(this, _healthBarRoot);
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