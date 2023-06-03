using System;

namespace Gameplay.Weapons
{
    public interface IWeaponOwner : IWeaponUser
    {
        IWeaponReadonly Weapon { get; }
        public event Action WeaponChanged;
        
        void ChangeWeapon(IWeaponBuilder weaponBuilder);
    }
}