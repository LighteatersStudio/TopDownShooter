using System;

namespace Gameplay.Weapons
{
    public interface IHaveAmmo
    {
        int RemainAmmo { get; }
        event Action RemainAmmoChanged;

    }
}