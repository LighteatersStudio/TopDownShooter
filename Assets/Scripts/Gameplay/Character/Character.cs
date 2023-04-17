using System;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform _viewRoot; 
        
        private CharacterAnimator _animator;
        private CharacterStats _stats;

        public float HealthRelative => _stats.HealthRelative;
        
        
        [Inject]
        public void Construct(StatsInfo statsInfo, Func<Transform, GameObject> viewFactoryMethod)
        {   
            _stats = new CharacterStats(statsInfo);
            
            _animator = viewFactoryMethod(_viewRoot).GetComponent<CharacterAnimator>();
            _animator.transform.SetZeroPositionAndRotation();
        }

        public void TakeDamage(float damage)
        {
            _stats.ApplyDamage(damage);
            _animator.PlayHitAnimation();
        }

        
        public class Factory : PlaceholderFactory<StatsInfo, Func<Transform, GameObject>, Character>
        {
        }
    }
}