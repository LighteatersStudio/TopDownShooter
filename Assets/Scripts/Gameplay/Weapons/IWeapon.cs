using System;

namespace Gameplay.Weapons
{
    public interface IWeapon : IWeaponReadonly
    {
        event Action ShotDone;
        event Action ReloadStarted;
        
        bool Shot();
        void Dispose();
        void Reload();
    }
}
