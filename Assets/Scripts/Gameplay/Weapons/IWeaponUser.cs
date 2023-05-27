using UnityEngine;

namespace Gameplay.Weapons
{
    public interface IWeaponUser
    {
        float AttackSpeed { get; }
        Transform WeaponRoot { get; }
        IWeapon Weapon { get; }
    }
}