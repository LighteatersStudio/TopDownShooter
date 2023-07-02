using System;
using Gameplay.Services.GameTime;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class EmptyWeapon : IWeapon
    {
        private const string Id= "EMPTY_WEAPON";

        public string WeaponType => Id;
        public IHaveAmmo Ammo { get; } = new EmptyAmmo();
        public event Action ShotDone;
        public event Action<ICooldown> ReloadStarted;
        public bool Shot()
        {
            return false;
        }

        public void Dispose()
        {
        }

        public void Reload()
        {
        }
    }
}