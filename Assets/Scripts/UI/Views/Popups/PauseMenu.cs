using Services.AppVersion.Level;
using Gameplay.Services.Pause;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PauseMenu : Popup
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _breakRunButton;
        
        private IPause _pauseManager;
        private IGameRun _gameRun;

        [Inject]
        public void Construct(IPause pause, IGameRun gameRun)
        {
            _pauseManager = pause;
            _gameRun = gameRun;
        }

      
        private void OnEnable()
        {
            _pauseManager.Paused = true;
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _breakRunButton.onClick.AddListener(OnBreakRunButtonClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
            _breakRunButton.onClick.RemoveListener(OnBreakRunButtonClicked);
        }

        private void OnCloseButtonClicked()
        {
            _pauseManager.Paused = false;
            Close();
        }
        
        private void OnBreakRunButtonClicked()
        {
            _gameRun.Finish();
            Close();
        }
    }
}
