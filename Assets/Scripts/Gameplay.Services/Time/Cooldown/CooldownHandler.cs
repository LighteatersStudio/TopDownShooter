using System;

namespace Gameplay.Services.GameTime
{
    public class CooldownHandler
    {
        private readonly ICooldown _cooldown;
        private readonly Action<float> _progressHandler;
        private readonly Action _finishHandler;

        public CooldownHandler(ICooldown cooldown, Action<float> progressHandler, Action finishHandler)
        {
            _cooldown = cooldown;
            _progressHandler = progressHandler;
            _finishHandler = finishHandler;

            _cooldown.ProgressChanged += OnProgressChanged;
            _cooldown.Completed += OnCompleted;
        }

        private void OnProgressChanged()
        {
            _progressHandler?.Invoke(_cooldown.Progress);
        }

        private void OnCompleted()
        {
            Break();
            
            _finishHandler?.Invoke();
        }
        
        public void Break()
        {
            _cooldown.ProgressChanged -= OnProgressChanged;
            _cooldown.Completed -= OnCompleted;
        }
    }
}