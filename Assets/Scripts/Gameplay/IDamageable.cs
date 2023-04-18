namespace Gameplay
{
    public interface IDamageable
    {
        float HealthRelative { get; }
        
        void TakeDamage(float damage);
    }
}