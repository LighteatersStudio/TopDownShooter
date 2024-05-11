using UnityEngine;
using Zenject;
using Meta.Level;
using TMPro;
using Gameplay.Services.Pause;
using Gameplay.Services.GameTime;
using UI.Framework;

namespace UI
{
    public class DeathMenu : Popup
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private IPause _pauseManager;
        private IGameRun _gameRun;
        private IGameTime _gameTime;

        [Inject]
        public void Construct(IPause pause, IGameRun gameRun, IGameTime gameTime)
        {
            _pauseManager = pause;
            _gameRun = gameRun;
            _gameTime = gameTime;
        }

        private void Start()
        {
            _pauseManager.Paused = true;
            UpdateTimeText();
        }

        private void UpdateTimeText()
        {
            _timerText.text = _gameTime.ConvertToString();
        }

        public void PressDoneButton()
        {
            _pauseManager.Paused = false;
            Close();
            _gameRun.Finish();
        }

        public class Factory : ViewFactory<DeathMenu>
        {
        }
    }
}