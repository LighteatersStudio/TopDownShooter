namespace Gameplay
{
    public class DamageCalculator : IDamageCalculator
    {
        /// <summary>
        /// Temporary solution
        /// </summary>
        public float Calculate(IAttackInfo attackInfo, IStats stats)
        {
            if (!stats.Imunne)
            {
                GetDamage(attackInfo);
            }

            return GetDamage(attackInfo);
        }

        private float GetDamage(IAttackInfo attackInfo)
        {
            float damage = 0;

            switch (attackInfo.TypeDamage)
            {
                case TypeDamage.Fire:
                    damage = attackInfo.Damage * 1.1f;
                    break;

                case TypeDamage.Ice:
                    damage = attackInfo.Damage * 1.2f;
                    break;

                case TypeDamage.Poison:
                    damage = attackInfo.Damage * 1.5f;
                    break;
            }

            return damage;
        }
    }
}