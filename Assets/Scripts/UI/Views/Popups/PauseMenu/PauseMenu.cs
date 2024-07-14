using Gameplay;
using Gameplay.Services.Pause;
using UI.Framework;
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
        private IGameState _gameState;

        [Inject]
        public void Construct(IPause pause, IGameState gameState)
        {
            _pauseManager = pause;
            _gameState = gameState;
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
            Close();
        }

        private void OnBreakRunButtonClicked()
        {
            _gameState.Death();
            Close();
        }

        public override void Close()
        {
            _pauseManager.Paused = false;
            base.Close();
        }

        public class Factory : ViewFactory<PauseMenu>
        {

        }
    }
}
