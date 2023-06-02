using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Zenject;
using Gameplay.Weapons;

namespace Gameplay
{
    public class Character : MonoBehaviour, ICharacter, IDamageable, IHaveHealth, ICanFire, ICanReload, IWeaponOwner
    {
        [Header("Component Roots")]
        [SerializeField] private Transform _viewRoot;
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private GameObject _reloadIndicator;
        
        [Header("Settings")]
        [SerializeField] private float _deathWaitTime = 10f;

        private DynamicMonoInitializer<Func<Transform, GameObject>> _initializer;
        private IDamageCalculator _damageCalculator;
        private CharacterStats _stats;
        private IWeapon _weapon;
        
        private bool IsDead => _stats.Health <= 0;
        
        public float HealthRelative => _stats.HealthRelative;
        public float MoveSpeed => _stats.MoveSpeed;
        
        public float AttackSpeed => _stats.AttackSpeed;
        public Transform WeaponRoot => _weaponRoot;
        public IWeaponReadonly Weapon => _weapon;
        
        public CharacterModelRoots ModelRoots { get; private set; }
        
        private Vector3 _fireDirection;

        public Vector3 LookDirection
        {
            get => _fireDirection;
            set
            {
                _fireDirection = value; 
                ChangeLookDirection(value);
            }
        }
        
        public event Action HealthChanged
        {
            add => _stats.HealthChanged += value;
            remove => _stats.HealthChanged -= value;
        }
        public event Action Damaged;
        public event Action Attacked;
        public event Action Dead;
        
        
        [Inject]
        public void Construct(StatsInfo statsInfo, Func<Transform, GameObject> viewFactoryMethod, IDamageCalculator damageCalculator, IWeapon weapon)
        {
            _damageCalculator = damageCalculator;
            _weapon = weapon;
            _stats = new CharacterStats(statsInfo);
            
            _initializer = new(viewFactoryMethod);
        }
        
        protected void Start()
        {
            _initializer.Initialize(Load);
        }

        private void Load(Func<Transform, GameObject> viewFactoryMethod)
        {
            ModelRoots = LoadViewAndGetRoots(viewFactoryMethod);
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

            var damage = _damageCalculator.Calculate(attackInfo, _stats);

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

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        private void ChangeLookDirection(Vector3 direction)
        {
            transform.forward = direction;
        }
        
        public void Fire()
        {
            if (_weapon.WasteBullet())
            {
                Attacked?.Invoke();    
            }
            else
            {
                _reloadIndicator.SetActive(true);
            }
        }

        public void Reload()
        {
            _weapon.Reload();
            _reloadIndicator.SetActive(false);
        }

        public class Factory : PlaceholderFactory<StatsInfo, Func<Transform, GameObject>, Character>
        {
        }
    }
}