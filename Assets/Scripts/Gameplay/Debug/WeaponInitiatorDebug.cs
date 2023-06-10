using Gameplay.Projectiles;
using Gameplay.Services.FX;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class WeaponInitiatorDebug : MonoBehaviour
    {
        [SerializeField] private Weapon _weaponPrefab;
        
        private IPlayer _player;
        private PlayingFX.Factory _fxFactory;
        private PoolProjectiles _poolProjectiles;

        
        [Inject]
        private void Construct(PlayingFX.Factory fxFactory, IPlayer player, PoolProjectiles poolProjectiles)
        {
            _fxFactory = fxFactory;
            _player = player;
            _poolProjectiles = poolProjectiles;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                var weaponBuilder = new WeaponBuilder(_weaponPrefab, _fxFactory, _poolProjectiles);
                _player.WeaponOwner.ChangeWeapon(weaponBuilder);
            }
        }
    }
}