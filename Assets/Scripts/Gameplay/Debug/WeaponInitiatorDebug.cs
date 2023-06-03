using Gameplay.Services.FX;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class WeaponInitiatorDebug : MonoBehaviour
    {
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private Character _character;
        
        private PlayingFX.Factory _fxFactory;

        
        [Inject]
        private void Construct(PlayingFX.Factory fxFactory)
        {
            _fxFactory = fxFactory;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                var weaponBuilder = new WeaponBuilder(_weaponPrefab, _fxFactory);
                _character.ChangeWeapon(weaponBuilder);
            }
        }
    }
}