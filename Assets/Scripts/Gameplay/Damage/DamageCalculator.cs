namespace Gameplay
{
    public class DamageCalculator : IDamageCalculator
    {
        public float Calculate(IAttackInfo attackInfo, IStats stats)
        {
            if (stats.Imunne)
            {
                return 0;
            }

            return GetDamage(attackInfo);
        }

        private float GetDamage(IAttackInfo attackInfo)
        {
            float damageMultiplicator = 0;

            switch (attackInfo.TypeDamage)
            {
                case TypeDamage.Fire:
                    damageMultiplicator = 1.1f;
                    break;

                case TypeDamage.Ice:
                    damageMultiplicator = 1.2f;
                    break;

                case TypeDamage.Poison:
                    damageMultiplicator = 1.5f;
                    break;
            }

            return damageMultiplicator * attackInfo.Damage;
        }
    }
}