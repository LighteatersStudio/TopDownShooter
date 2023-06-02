using Gameplay.Services.FX;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class WeaponBuilder
    {
        private Weapon _weaponPrefab;
        private PlayingFX.Factory _fxFactory;
        private IWeaponUser _user;

        
        public WeaponBuilder(Weapon weaponPrefab, PlayingFX.Factory fxFactory)
        {
            _weaponPrefab = weaponPrefab;
            _fxFactory = fxFactory;
        }

        public IWeapon CreateWeapon(IWeaponUser user)
        {
            _user = user;

            var weapon = GameObject.Instantiate(_weaponPrefab);
            weapon.Construct(_fxFactory, _user);

            return weapon;
        }
    }
}