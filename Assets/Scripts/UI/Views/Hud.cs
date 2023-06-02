using Gameplay;
using UI.Framework;
using UnityEngine;
using Zenject;

namespace UI
{
    public class Hud : View
    {
        [SerializeField] private WeaponView _weaponView;

        private IPlayer _player;
        
        
        [Inject]
        public void Construct(IPlayer player)
        {
            _player = player;
        }
        
        private void Start()
        {
            ChangeViewWeapon();
            _player.WeaponOwner.ChangeWeaponUIView += ChangeViewWeapon;
        }
        
        private void OnDestroy()
        {
            _player.WeaponOwner.ChangeWeaponUIView -= ChangeViewWeapon;
        }

        private void ChangeViewWeapon()
        {
            _weaponView.SetupWeaponOwner(_player.WeaponOwner);
        }
    }
}