using System;
using Services.AppVersion.Level;
using Gameplay.Services.GameTime;
using UnityEngine;
using Zenject;
using Gameplay.Services.Pause;
using TMPro;

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
            UpdateTimeText(_timerText, _gameTime.Value);
        }

        private void UpdateTimeText(TextMeshProUGUI text, float time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;
            var seconds = timeSpan.Seconds;

            text.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
            
            if (hours > 0)
            {
                text.text = string.Format("{0:D2}", hours) + text.text;
            }
        }
        
        public void ClickDoneLevelButton()
        {
            _gameRun.Finish();
        }
    }
}