using Meta.Level;
using Gameplay.Services.GameTime;
using UnityEngine;
using Zenject;
using Gameplay.Services.Pause;
using TMPro;
using UI.Framework;
using UnityEngine.UI;

namespace UI
{
    public class WinLevelMenu : Popup
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Button _doneButton;

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
            _doneButton.onClick.AddListener(ClickDoneLevelButton);
            _pauseManager.Paused = true;
            UpdateTimeText();
        }

        private void UpdateTimeText()
        {
            _timerText.text = _gameTime.ConvertToString();
        }

        private void ClickDoneLevelButton()
        {
            _gameRun.NextLevel();
        }

        private void OnDestroy()
        {
            _doneButton.onClick.RemoveListener(ClickDoneLevelButton);
        }

        public class Factory : ViewFactory<WinLevelMenu>
        {
        }
    }
}