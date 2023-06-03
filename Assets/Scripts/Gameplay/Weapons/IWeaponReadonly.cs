using System;

namespace Gameplay.Weapons
{
    public interface IWeaponReadonly
    {
        string WeaponType { get; }

        event Action Disposed;
    }
}