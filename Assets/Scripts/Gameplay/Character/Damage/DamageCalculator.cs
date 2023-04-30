namespace Gameplay
{
    public class DamageCalculator 
    {
        public float CalculateDamage(float damage, float health, TypeDamage typeDamage)
        {
            health -= damage;

            return health;
        }
    }
}