using System;

namespace Gameplay
{
    public class CharacterStats
    {
        private DamageCalculator _damageCalculator;
        private TypeDamage _typeDamage;
        
        public float MaxHealth { get; }
        
        public float Health { get; private set; }
        
        //public float Armor { get; private set; }
        
        public float MoveSpeed { get; private set; }
        
        public float HealthRelative => Health / MaxHealth;
        
        public event Action HealthChanged;
        
        
        public CharacterStats(StatsInfo info)
        {
            _damageCalculator = new DamageCalculator();
            MaxHealth = info.MaxHealth;
            Health = info.Health;
            MoveSpeed = info.MoveSpeed;
        }
        
        public float ApplyDamage(float damage)
        {
            Health = _damageCalculator.CalculateDamage(damage, Health, _typeDamage);
            
            HealthChanged?.Invoke();

            return damage;
        }
    }
}