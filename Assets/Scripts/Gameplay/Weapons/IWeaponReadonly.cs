using System;

namespace Gameplay.Weapons
{
    public interface IWeaponReadonly : IReloaded
    {
        string WeaponType { get; }
        
        int RemainAmmo { get; }
        
        event Action ShotDone;
    }
}