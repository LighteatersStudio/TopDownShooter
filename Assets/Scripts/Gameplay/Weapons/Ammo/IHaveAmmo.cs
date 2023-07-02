using System;
using Gameplay.Services.GameTime;

namespace Gameplay.Weapons
{
    public interface IHaveAmmo
    {
        int RemainAmmo { get; }
        bool Reloading { get; }
        
        event Action RemainAmmoChanged;
        event Action<ICooldown> Reloaded;

    }
}