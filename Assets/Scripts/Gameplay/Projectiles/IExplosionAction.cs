namespace Gameplay.Projectiles
{
    public interface IExplosionAction
    {
        void Blast(IFriendFoeSystem friendFoeSystem, IAttackInfo attackInfo);
    }
}