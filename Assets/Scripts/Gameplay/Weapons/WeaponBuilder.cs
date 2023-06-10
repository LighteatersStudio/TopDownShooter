using Gameplay.Projectiles;
using Gameplay.Services.FX;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class WeaponBuilder : IWeaponBuilder
    {
        private readonly Weapon _prefab;
        private readonly PlayingFX.Factory _fxFactory;
        private readonly PoolProjectiles _poolProjectiles;

        
        public WeaponBuilder(Weapon prefab, PlayingFX.Factory fxFactory, PoolProjectiles poolProjectiles)
        {
            _prefab = prefab;
            _fxFactory = fxFactory;
            _poolProjectiles = poolProjectiles;
        }

        public IWeapon Create(IWeaponUser weaponUser)
        {
            var weapon = Object.Instantiate(_prefab);
            weapon.Construct(_fxFactory, weaponUser,_poolProjectiles);

            return weapon;
        }
    }
}