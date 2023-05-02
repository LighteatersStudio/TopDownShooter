namespace Gameplay
{
    public class DamageCalculator
    {
        private float _damage;
        
        /// <summary>
        /// Temporary solution
        /// </summary>
        public float Calculate(IAttackInfo attackInfo, IStats stats)
        {
            if (!stats.Imunne)
            {
                switch (attackInfo.TypeDamage)
                {
                    case TypeDamage.Fire:
                        _damage = attackInfo.Damage * 1.1f;
                        break;

                    case TypeDamage.Ice:
                        _damage = attackInfo.Damage * 1.2f;
                        break;

                    case TypeDamage.Poison:
                        _damage = attackInfo.Damage * 1.5f;
                        break;
                }
            }
            else
            {
                _damage = 0;
            }
            
            return _damage;
        }
    }
}