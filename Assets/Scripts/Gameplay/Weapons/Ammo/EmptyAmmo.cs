using System;
using Gameplay.Services.GameTime;

namespace Gameplay.Weapons
{
    public class EmptyAmmo : IHaveAmmo
    {
        public int RemainAmmo => 0;
        public bool Reloading => false;
        public event Action RemainAmmoChanged;
        public event Action<ICooldown> Reloaded;
    }
}