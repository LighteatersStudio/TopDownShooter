using System;
using Gameplay.View;
using UnityEngine;
using Utility;
using Zenject;

namespace Gameplay
{
    public class Character : MonoBehaviour, ICharacter, IDamageable, IHaveHealth
    {
        [SerializeField] private Transform _viewRoot;
        [SerializeField] private float _deathWaitTime = 10f; 
        
        private DynamicMonoInitializer<StatsInfo, Func<Transform, GameObject>, HealthBar.Factory, CharacterFX.Factory> _initializer;
        private DamageCalculator _damageCalculator;
        private CharacterStats _stats;
        private bool IsDead => _stats.Health <= 0;
        public float HealthRelative => _stats.HealthRelative;

        public event Action HealthChanged
        {
            add => _stats.HealthChanged += value;
            remove => _stats.HealthChanged -= value;
        }
        public event Action Damaged;
        public event Action Dead;
        

        [Inject]
        public void Construct(StatsInfo statsInfo, Func<Transform, GameObject> viewFactoryMethod,
            HealthBar.Factory healthBarFactory, CharacterFX.Factory fxFactory)
        {
            _initializer = new(
                statsInfo,
                viewFactoryMethod,
                healthBarFactory,
                fxFactory);
        }

        protected void Start()
        {
            _initializer.Initialize(Load);
        }

        private void Load(StatsInfo info, Func<Transform, GameObject> viewFactoryMethod, HealthBar.Factory healthBarFactory, CharacterFX.Factory fxFactory)
        {
            _stats = new CharacterStats(info);
            _damageCalculator = new DamageCalculator();

            var modelRoot = LoadViewAndGetRoots(viewFactoryMethod);
            
            healthBarFactory.Create(this, modelRoot.Head);
            fxFactory.Create(this, modelRoot.Head);
        }

        private CharacterModelRoots LoadViewAndGetRoots(Func<Transform, GameObject> viewFactoryMethod)
        {
            var model = viewFactoryMethod(_viewRoot);
            model.GetComponent<CharacterAnimator>().Construct(this);
            return model.GetComponent<CharacterModelRoots>();
        }

        public void TakeDamage(IAttackInfo attackInfo)
        {
            if (IsDead)
            {
                return;
            }

            var damage = _damageCalculator.Calculate(attackInfo, _stats.Stats);

            if (damage > 1e-5)
            {
                _stats.ApplyDamage(damage);
                Damaged?.Invoke();
            }

            if (IsDead)
            {
                Death();
            }
        }

        private void Death()
        {
            Dead?.Invoke();
            
            Destroy(gameObject, _deathWaitTime);
        }
        
        
        public class Factory : PlaceholderFactory<StatsInfo, Func<Transform, GameObject>, Character>
        {
        }
    }
}