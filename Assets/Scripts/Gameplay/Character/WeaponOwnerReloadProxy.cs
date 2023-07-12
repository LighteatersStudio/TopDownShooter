using System;
using Gameplay.Services.GameTime;
using Gameplay.Weapons;

namespace Gameplay
{
    public class WeaponOwnerReloadProxy : IReloaded
    {
        private readonly IWeaponOwner _weaponOwner;

        private IWeaponReadonly _currentWeapon;
        
        public event Action<ICooldown> ReloadStarted;
        
        public WeaponOwnerReloadProxy(IWeaponOwner weaponOwner)
        {
            _weaponOwner = weaponOwner;
            _weaponOwner.WeaponChanged += WeaponChanged;
        }

        private void WeaponChanged()
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.ReloadStarted -= Reloaded;    
            }
            
            _currentWeapon =_weaponOwner.Weapon;
            _currentWeapon.ReloadStarted += Reloaded;
        }

        private void Reloaded(ICooldown cooldown)
        {
            ReloadStarted?.Invoke(cooldown);
        }

    }
}