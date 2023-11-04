using System;
using UnityEngine;
using Services.Utility;
using Zenject;
using Gameplay.Weapons;

namespace Gameplay
{
    public class Character : MonoBehaviour, ICharacter, IDamageable, IHaveHealth, ICanFire, IWeaponOwner, ICanReload
    {
        [Header("Component Roots")]
        [SerializeField] private Transform _viewRoot;
        [SerializeField] private Transform _projectileRoot;
        
        [Header("Settings")]
        [SerializeField] private float _deathWaitTime = 10f;

        private DynamicMonoInitializer<Func<Transform, GameObject>> _initializer;
        private IDamageCalculator _damageCalculator;
        private CharacterStats _stats;
        private IWeapon _weapon;
        private Transform _weaponRoot;
        
        private Vector3 _fireDirection;

        private bool IsDead => _stats.Health <= 0;
        
        public float HealthRelative => _stats.HealthRelative;
        public float MoveSpeed => _stats.MoveSpeed;
        
        public float AttackSpeed => _stats.AttackSpeed;

        public Transform WeaponRoot => _weaponRoot;
        public Transform ProjectileRoot => _projectileRoot;
        public IFriendOrFoeTag FriendOrFoeTag { get; private set; }
        public IWeaponReadonly Weapon => _weapon;
        public CharacterModelRoots ModelRoots { get; private set; }
        public Vector3 LookDirection
        {
            get => _fireDirection;
            set
            {
                _fireDirection = value; 
                ChangeLookDirection(_fireDirection);
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
        public event Action WeaponChanged;
      
        
        [Inject]
        public void Construct(StatsInfo statsInfo, Func<Transform, GameObject> viewFactoryMethod,
            IDamageCalculator damageCalculator, IWeapon weapon, IFriendOrFoeTag friendOrFoeTag)
        {
            _damageCalculator = damageCalculator;
            ApplyNewWeapon(weapon);
            _stats = new CharacterStats(statsInfo);
            FriendOrFoeTag = friendOrFoeTag;

            _initializer = new(viewFactoryMethod);
            
            _initializer.Initialize(Load);
        }

        private void Start()
        {
            Transform requiredObject = FindObjectInHierarchy(_viewRoot, "R");

            if (requiredObject != null)
            {
                _weaponRoot = requiredObject;
            }
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

        private Transform FindObjectInHierarchy(Transform parentObject, string objectName)
        {
            Transform foundObject = parentObject.transform.Find(objectName);

            if (foundObject != null)
            {
                return foundObject;
            }

            foreach (Transform child in parentObject.transform)
            {
                Transform foundInChildren = FindObjectInHierarchy(child, objectName);
                
                if (foundInChildren != null)
                {
                    return foundInChildren;
                }
            }

            return null;
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

        public void RecoverHealth(float hpAmount)
        {
            _stats.Heal(hpAmount);
        }

        private void Death()
        {
            Dead?.Invoke();
            
            Destroy(gameObject, _deathWaitTime);
        }

        private void ChangeLookDirection(Vector3 direction)
        {
            transform.forward = direction.normalized;
        }
        
        public void Fire()
        {
            if (_weapon.Shot())
            {
                Attacked?.Invoke();    
            }
        }
        
        public void Reload()
        {
            _weapon.Reload();
        }    

        public void ChangeWeapon(IWeapon newWeapon)
        {
            ApplyNewWeapon(newWeapon);
            WeaponChanged?.Invoke();
        }

        private void ApplyNewWeapon(IWeapon newWeapon)
        {
            _weapon?.Dispose();
            _weapon = newWeapon;
        }

        public void Rotate(float angle)
        {
            transform.Rotate(Vector3.up * angle);
        }

        public class Factory : PlaceholderFactory<ICharacterSettings, IFriendOrFoeTag, Character>
        {
        }
    }
}