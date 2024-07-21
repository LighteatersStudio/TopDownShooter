using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.Pause
{
    public class PauseManager : IPause, IInitializable, IDisposable
    {
        private float _lastTimeScale;
        private bool _paused;

        public bool Paused
        {
            get => _paused;
            set
            {
                if (_paused == value)
                {
                    return;
                }

                ProcessTimeScale(_paused, value);
                _paused = value;
                PauseChanged?.Invoke(this);
            }
        }

        public event Action<IPause> PauseChanged;

        public void Initialize()
        {
            _lastTimeScale = Time.timeScale;
        }

        private void ProcessTimeScale(bool oldPaused, bool newPaused)
        {
            if (!oldPaused)
            {
                _lastTimeScale = Time.timeScale;
            }

            Time.timeScale = newPaused ? 0 : _lastTimeScale;
            
            Debug.Log($"Time scale changed to: {Time.timeScale}");
        }

        public void Dispose()
        {
            Time.timeScale = 1;
            
            Debug.Log($"Time scale changed to: {Time.timeScale}");
        }
    }
}