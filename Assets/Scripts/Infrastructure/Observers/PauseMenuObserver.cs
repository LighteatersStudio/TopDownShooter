using UI;
using UI.Framework;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class PauseMenuObserver : MonoBehaviour
    {
        private IUIRoot _uiRoot;
        private PauseMenu _pauseMenu;
    
        [Inject]
        public void Construct(IUIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseMenu();
            }
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
                _pauseMenu = _uiRoot.Open<PauseMenu>();
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
