using System;

namespace Gameplay.Weapons
{
    public interface IWeaponOwner : IWeaponUser
    {
        IWeaponReadonly Weapon { get; }
        public event Action<IWeaponReadonly, IWeaponReadonly> WeaponChanged;
    }
}