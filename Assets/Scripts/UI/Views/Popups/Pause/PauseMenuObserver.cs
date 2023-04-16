using UnityEngine;
using Zenject;

namespace UI
{
    public class PauseMenuObserver : MonoBehaviour
    {
        private IUIRoot _uiRoot;
        private PauseMenu _pauseMenu;
        private PauseManager _pauseManager;
    
        [Inject]
        public void Construct(IUIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_pauseMenu)
                {
                    Debug.Log("Pause menu observer: Pause menu closed");
                    _pauseMenu.Close();
                    _pauseMenu = null;
                }
                else
                {
                    _pauseMenu = _uiRoot.Open<PauseMenu>();
                    Debug.Log("Pause menu observer: Pause menu opened");
                }
            }
        }
    }
}
