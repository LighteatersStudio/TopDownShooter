using Gameplay.Services.FX;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class WeaponBuilder : IWeaponBuilder
    {
        private readonly Weapon _prefab;
        private readonly PlayingFX.Factory _fxFactory;

        
        public WeaponBuilder(Weapon prefab, PlayingFX.Factory fxFactory)
        {
            _prefab = prefab;
            _fxFactory = fxFactory;
        }

        public IWeapon Create(IWeaponUser weaponUser)
        {
            var weapon = Object.Instantiate(_prefab);
            weapon.Construct(_fxFactory, weaponUser);

            return weapon;
        }
    }
}