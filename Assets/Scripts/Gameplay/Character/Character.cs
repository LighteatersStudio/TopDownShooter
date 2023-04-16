using System;
using UnityEngine;

namespace Gameplay
{
    public class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform _viewRoot; 
        
        private CharacterAnimator _animator;
        private CharacterStats _stats;

        public float HealthRelative => _stats.HealthRelative;
        
        
        public void Load(StatsInfo statsInfo, Func<Transform, GameObject> viewFactoryMethod)
        {
            _stats = new CharacterStats(statsInfo);
            _animator = viewFactoryMethod(_viewRoot).GetComponent<CharacterAnimator>();
        }

        public void TakeDamage(float damage)
        {
            _stats.ApplyDamage(damage);
            _animator.PlayHitAnimation();
        }
    }
}