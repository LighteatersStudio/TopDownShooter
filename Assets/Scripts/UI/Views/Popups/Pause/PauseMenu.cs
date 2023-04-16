using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : PopupBase
    {
        [SerializeField] private Button _closeButton;
        
        private IUIRoot _uiRoot;
        private PauseManager _pauseManager;
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Pause);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Pause);
        }

        private void Pause()
        {
            _pauseManager.Paused = !_pauseManager.Paused;
        }
    }
}
