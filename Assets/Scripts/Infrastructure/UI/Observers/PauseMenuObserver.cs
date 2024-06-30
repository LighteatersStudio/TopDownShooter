using System;
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

        [Inject]
        public void Construct(PauseMenu.Factory pauseMenuFactory, IUIInputController uiInputController)
        {
            _pauseMenuFactory = pauseMenuFactory;
            uiInputController.CancelChanged += TogglePauseMenu;
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
            if (!_pauseMenu.Avaliable())
            {
                return;
            }

            _pauseMenu.Closed -= OnPauseMenuClosed;
        }
    }
}