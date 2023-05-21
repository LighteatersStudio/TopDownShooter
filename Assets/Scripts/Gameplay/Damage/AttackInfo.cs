﻿namespace Gameplay
{
    public class AttackInfo : IAttackInfo
    {
        public float Damage { get; }
        public TypeDamage TypeDamage { get; }

        
        public AttackInfo(float damage, TypeDamage typeDamage)
        {
            Damage = damage;
            TypeDamage = typeDamage;
        }
    }
}