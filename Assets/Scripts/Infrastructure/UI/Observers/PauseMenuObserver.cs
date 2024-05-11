using UI;
using UI.Framework;
using UnityEngine;
using Zenject;
using Gameplay.Services.Input;

namespace Infrastructure.UI
{
    public class PauseMenuObserver : MonoBehaviour
    {
        private PauseMenu _pauseMenu;
        private IUIRoot _uiRoot;

        [Inject]
        public void Construct(IUIRoot uiRoot, IUIInputController uiInputController)
        {
            _uiRoot = uiRoot;
            uiInputController.CancelChanged += TogglePauseMenu;
        }

        private void TogglePauseMenu()
        {
            if (_pauseMenu)
            {
                _pauseMenu.Close();
                _pauseMenu = null;
            }
            else
            {
                // _pauseMenu = _uiRoot.Open<PauseMenu>();
                _pauseMenu.Closed += OnPauseMenuClosed;
            }
        }

        private void OnPauseMenuClosed(IView view)
        {
            _pauseMenu.Closed -= OnPauseMenuClosed;
            _pauseMenu = null;
        }
    }
}