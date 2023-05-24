using Gameplay;
using UI;
using UI.Framework;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class PlayerDeathObserver 
    {
        private IUIRoot _uiRoot;
        private IPlayer _player;
        
        [Inject]
        public void Construct(IUIRoot uiRoot, IPlayer player)
        {
            _uiRoot = uiRoot;
            _player = player;
            
            _player.Dead += ToggleDeathMenu;
        }

        private void ToggleDeathMenu()
        {
            _uiRoot.Open<DeathMenu>();
        }
    }
}