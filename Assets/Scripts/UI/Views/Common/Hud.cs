using Gameplay;
using UI.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views.Common
{
    public class Hud : View
    {
        [SerializeField] private WeaponView _weaponView;
        [SerializeField] private Button _pauseMenuButton;

        private IPlayer _player;
        private PauseMenuObserver _pauseMenuObserver;

        [Inject]
        public void Construct(IPlayer player, PauseMenuObserver pauseMenuObserver)
        {
            _player = player;
            _pauseMenuObserver = pauseMenuObserver;
        }

        private void Start()
        {
            InitWeapon();
            _pauseMenuButton.onClick.AddListener(OnPauseClicked);
        }

        private void OnPauseClicked()
        {
            _pauseMenuObserver.ManualTogglePauseMenu();
        }

        private void InitWeapon()
        {
            _weaponView.SetupOwner(_player.WeaponOwner);
        }

        private void OnDestroy()
        {
            _pauseMenuButton.onClick.RemoveListener(OnPauseClicked);
        }

        public class Factory : ViewFactory<Hud>
        {
        }
    }
}