using Gameplay;
using UI.Framework;
using UnityEngine;
using Zenject;
using Gameplay.Weapons;

namespace UI
{
    public class Hud : View
    {
        [SerializeField] private WeaponView _weaponView;

        private IPlayer _player;
        private WeaponInitiator _weaponInitiator;
        
        
        [Inject]
        public void Construct(IPlayer player, WeaponInitiator weaponInitiator)
        {
            _player = player;
            _weaponInitiator = weaponInitiator;
        }
        
        private void Start()
        {
            ChangeViewWeapon();
            _weaponInitiator.ChangeWeaponUIView += ChangeViewWeapon;
        }
        
        private void OnDestroy()
        {
            _weaponInitiator.ChangeWeaponUIView -= ChangeViewWeapon;
        }

        private void ChangeViewWeapon()
        {
            _weaponView.SetupWeaponOwner(_player.WeaponOwner);
        }
    }
}