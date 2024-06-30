using UI;
using UI.Framework;
using UnityEngine;
using Zenject;
using Gameplay.Services.Input;

namespace Infrastructure.UI
{
    public class PauseMenuObserver : MonoBehaviour
    {
        private IView _pauseMenu;
        private PauseMenu.Factory _pauseMenuFactory;
        private IUIInputController _uiInputController;

        [Inject]
        public void Construct(PauseMenu.Factory pauseMenuFactory, IUIInputController uiInputController)
        {
            _pauseMenuFactory = pauseMenuFactory;
            _uiInputController = uiInputController;
        }

        private void Start()
        {
            _uiInputController.CancelChanged += TogglePauseMenu;
        }

        private void TogglePauseMenu()
        {
            if (_pauseMenu.Avaliable())
            {
                _pauseMenu.Close();
                _pauseMenu = null;
            }
            else
            {
                _pauseMenu = _pauseMenuFactory.Open();
                _pauseMenu.Closed += OnPauseMenuClosed;
            }
        }

        private void OnPauseMenuClosed(IView view)
        {
            _pauseMenu.Closed -= OnPauseMenuClosed;
            _pauseMenu = null;
        }

        private void OnDestroy()
        {
            _uiInputController.CancelChanged -= TogglePauseMenu;
            if (_pauseMenu.Avaliable())
            {
                _pauseMenu.Closed -= OnPauseMenuClosed;
            }
        }
    }
}