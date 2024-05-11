using Gameplay;
using UI;
using UI.Framework;
using UnityEngine;
using Zenject;

namespace Infrastructure.UI
{
    public class PlayerDeathObserver
    {
        private DeathMenu.Factory _deathMenuFactory;
        private IPlayer _player;

        [Inject]
        public void Construct(DeathMenu.Factory deathMenuFactory, IPlayer player)
        {
            _deathMenuFactory = deathMenuFactory;
            _player = player;

            _player.Dead += ToggleDeathMenu;
        }

        private void ToggleDeathMenu()
        {
            _deathMenuFactory.Open();
        }
    }
}