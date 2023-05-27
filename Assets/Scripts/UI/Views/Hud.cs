using Gameplay;
using Gameplay.Weapons;
using UI.Framework;
using UnityEngine;
using Zenject;

namespace UI
{
    public class Hud : View
    {
        private IWeapon _player;
        
        [Inject]
        public void Construct(IWeapon player)
        {
            _player = player;
        }

        private void CheckWeapon()
        {
            //_player.;
        }
    }
}