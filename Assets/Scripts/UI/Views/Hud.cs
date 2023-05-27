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
            _weaponView.Construct(_player.WeaponOwner);
        }
    }
}