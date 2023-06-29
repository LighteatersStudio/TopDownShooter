using System;

namespace Gameplay.Services.GameTime
{
    public class Cooldown : ICooldown
    {
        private readonly float _duration;
        private readonly Action _finishHandler;
        private readonly ITicker _ticker;
        
        private float _timer;

        public float Progress => _timer / _duration;
        public bool IsFinish { get; private set; }
        
        public event Action ProgressChanged;
        public event Action Completed;

        private Cooldown()
        {
        }
        
        public Cooldown(float duration, ITicker ticker, Action finishHandler = null)
        {
            _duration = duration;
            _ticker = ticker;
            _finishHandler = finishHandler;
            
            Launch();
        }

        private void Launch()
        {
            _timer = _duration;
            _ticker.Tick += Update;
        }

        private void Update(float deltaTime)
        {
            if (_timer < 0)
            {
                return;
            }
            
            _timer -= deltaTime;
            ProgressChanged?.Invoke();

            if (_timer <= 0)
            {
                Finish();
            }
        }

        private void Finish()
        {
            IsFinish = true;
            
            _ticker.Tick -= Update;
            
            _finishHandler?.Invoke();
            Completed?.Invoke();
        }

        public static ICooldown NewFinished()
        {
            return new Cooldown { IsFinish = true };
        }
    }
}