using Gameplay;
using UI.Framework;
using UnityEngine;
using Zenject;

namespace UI.Views.Common
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
            InitWeapon();
        }

        private void InitWeapon()
        {
            _weaponView.SetupOwner(_player.WeaponOwner);
        }
    }
}