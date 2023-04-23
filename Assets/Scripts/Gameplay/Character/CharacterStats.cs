using System;

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
            Health -= damage;
            
            HealthChanged?.Invoke();
        }
    }
}