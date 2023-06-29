using System;
using Gameplay.Services.GameTime;

namespace Gameplay.Weapons
{
    public interface IWeapon : IWeaponReadonly
    {
        event Action ShotDone;
        event Action<ICooldown> ReloadStarted;
        
        bool Shot();
        void Dispose();
        void Reload();
    }
}
