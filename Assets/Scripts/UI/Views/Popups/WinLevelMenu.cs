using System;
using Level;
using Services.GameTime;
using UnityEngine;
using Zenject;
using Services.Pause;
using TMPro;

namespace UI
{
    public class WinLevelMenu : PopupBase
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

            if (hours != 0)
            {
                text.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            }
            else
            {
                text.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }
        }
        
        public void ClickDoneLevelButton()
        {
            _gameRun.Finish();
        }
    }
}