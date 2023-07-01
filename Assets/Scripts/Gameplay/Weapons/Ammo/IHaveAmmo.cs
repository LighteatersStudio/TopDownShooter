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

        public class Fake : IHaveAmmo
        {
            public int RemainAmmo => 0;
            public bool Reloading => false;
            public event Action RemainAmmoChanged;
            public event Action<ICooldown> Reloaded;
        }
    }
}