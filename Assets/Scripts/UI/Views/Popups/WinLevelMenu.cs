using Meta.Level;
using Gameplay.Services.GameTime;
using UnityEngine;
using Zenject;
using Gameplay.Services.Pause;
using TMPro;
using UI.Framework;

namespace UI
{
    public class WinLevelMenu : Popup
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private IGameRun _gameRun;
        private IPause _pauseManager;
        private IGameTime _gameTime;

        [Inject]
        public void Construct(IGameRun gameRun, IPause pause, IGameTime gameTime)
        {
            _gameRun = gameRun;
            _pauseManager = pause;
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

        public void ClickDoneLevelButton()
        {
            _gameRun.NextLevel();
        }

        public class Factory : ViewFactory<WinLevelMenu>
        {
        }
    }
}