namespace Gameplay
{
    public interface IAttackInfo
    {
       float Damage { get; }
       TypeDamage TypeDamage { get; }
       IFriendOrFoeTag FriendOrFoeTag { get; }
    }
}