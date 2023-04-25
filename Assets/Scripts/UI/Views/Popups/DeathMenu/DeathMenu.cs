using System;
using UnityEngine;
using Zenject;
using Level;
using TMPro;
using Services.Pause;
using Services.GameTime;

namespace UI
{
    public class DeathMenu : PopupBase
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

        public void PressDoneButton()
        {
            _gameRun.Finish();
        }
    }
}