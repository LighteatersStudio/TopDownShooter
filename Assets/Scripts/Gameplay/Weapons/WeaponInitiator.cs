using System;
using Gameplay.Services.FX;
using UnityEngine;
using Zenject;

namespace Gameplay.Weapons
{
    public class WeaponInitiator : MonoBehaviour
    {
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private Character _character;
        
        private PlayingFX.Factory _fxFactory;
        
        public event Action ChangeWeaponUIView;

        
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