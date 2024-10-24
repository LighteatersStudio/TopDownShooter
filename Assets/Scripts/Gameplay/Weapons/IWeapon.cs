using System;
using Gameplay.Services.GameTime;

namespace Gameplay.Weapons
{
    public interface IWeapon : IWeaponReadonly
    {
        bool Shot();
        void Dispose();
        void Reload();

        public class Fake : IWeapon
        {
            private const string Id= "EMPTY_WEAPON";

            public string WeaponType => Id;
            public int RemainAmmo => 0;
            public event Action ShotDone;
            public event Action<ICooldown> ReloadStarted;
            public bool Shot() => false;
            public void Dispose(){}
            public void Reload(){}
        }
    }
}
