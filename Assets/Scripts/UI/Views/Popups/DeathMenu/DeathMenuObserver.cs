using Gameplay;
using UnityEngine;
using Zenject;

namespace UI
{
    public class DeathMenuObserver: MonoBehaviour
    {
        private IUIRoot _uiRoot;
        private IPlayer _player;
        
        [Inject]
        public void Construct(IUIRoot uiRoot, IPlayer player)
        {
            _uiRoot = uiRoot;
            _player = player;
        }

        
        private void OnEnable()
        {
            _player.Dead += ToggleDeathMenu;
        }
        
        private void OnDisable()
        {
            _player.Dead += ToggleDeathMenu;
        }

        private void ToggleDeathMenu()
        {
            _uiRoot.Open<DeathMenu>();
        }
    }
}