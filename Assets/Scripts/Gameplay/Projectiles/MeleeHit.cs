namespace Gameplay.Projectiles
{
    public class MeleeHit : Projectile
    {
        protected override void LaunchInternal()
        {
            HandleNonTaggedCollision();
        }
    }
}