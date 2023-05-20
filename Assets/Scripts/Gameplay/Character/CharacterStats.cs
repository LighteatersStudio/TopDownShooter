using System;
using UnityEngine;

namespace Gameplay
{
    public class CharacterStats : IStats

    {
        public float MaxHealth { get; }
        public float Health { get; private set; }
        public float MoveSpeed { get; private set; }
        public float AttackSpeed { get; private set; }
        public float HealthRelative => Health / MaxHealth;
        public bool Imunne { get; }

        public event Action HealthChanged;


        public CharacterStats(StatsInfo info)
        {
            MaxHealth = info.MaxHealth;
            Health = info.Health;
            MoveSpeed = info.MoveSpeed;
            AttackSpeed = info.AttackSpeed;
        }

        public void ApplyDamage(float damage)
        {
            Health = Mathf.Clamp(Health - damage, 0, MaxHealth);

            HealthChanged?.Invoke();
        }
    }
}
