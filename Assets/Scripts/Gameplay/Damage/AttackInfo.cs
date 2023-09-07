namespace Gameplay
{
    public class AttackInfo : IAttackInfo
    {
        public float Damage { get; }
        public TypeDamage TypeDamage { get; }
        
        public IFriendOrFoeTag FriendOrFoeTag { get; }


        public AttackInfo(float damage, TypeDamage typeDamage, IFriendOrFoeTag friendOrFoeTag)
        {
            Damage = damage;
            TypeDamage = typeDamage;
            FriendOrFoeTag = friendOrFoeTag;
        }
    }
}