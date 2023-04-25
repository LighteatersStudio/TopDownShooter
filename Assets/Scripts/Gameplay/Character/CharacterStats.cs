using System;
using UnityEngine;

namespace Gameplay
{
    public class CharacterStats
    {
        public float MaxHealth { get; }
        public float Health { get; private set; }
        
        public float HealthRelative => Health / MaxHealth;
        
        public event Action HealthChanged;
        
        
        public CharacterStats(StatsInfo info)
        {
            MaxHealth = info.MaxHealth;
            Health = info.Health;
        }
        
        public void ApplyDamage(float damage)
        {
            Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
            
            HealthChanged?.Invoke();
        }
    }
}