using UnityEngine;

namespace Gameplay.Weapons
{
    public interface IWeaponUser
    {
        float AttackSpeed { get; }
        Transform WeaponRoot { get; }
        Transform ProjectileRoot { get; }
        IFriendOrFoeTag FriendOrFoeTag { get; }
    }
}