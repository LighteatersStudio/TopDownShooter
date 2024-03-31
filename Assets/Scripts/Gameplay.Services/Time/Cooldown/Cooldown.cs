using System;
using UnityEngine;

namespace Gameplay.Services.GameTime
{
    public class Cooldown : ICooldown
    {
        private readonly float _duration;
        private readonly ITicker _ticker;
        private readonly Action _finishHandler;
        
        private float _timer;

        public float Progress => _timer / _duration;
        public bool IsFinish { get; private set; }
        
        public event Action ProgressChanged;
        public event Action Completed;

        public Cooldown(float duration, ITicker ticker, Action finishHandler)
        {
            _duration = duration;
            _ticker = ticker;
            _finishHandler = finishHandler;
        }

        public void Launch()
        {
            if (IsFinish)
            {
                Debug.LogError("Launch failure. Cooldown already finished!");
                return;
            }
            
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

        public void ForceFinish()
        {
            _timer = 0;
            Finish();
        }

        
        public class Factory
        {
            private readonly ITicker _commonTicker;

            public Factory(ITicker commonTicker)
            {
                _commonTicker = commonTicker;
            }
            
            public virtual Cooldown Create(float duration, ITicker ticker, Action finishHandler = null)
            {
                return new Cooldown(duration, ticker, finishHandler);
            }
            
            public virtual Cooldown CreateWithCommonTicker(float duration, Action finishHandler = null)
            {
                return Create(duration, _commonTicker, finishHandler);
            }
            
            public virtual Cooldown CreateFinished()
            {
                var instance = Create(1, new ITicker.Fake());
                instance.Launch();
                instance.ForceFinish();
                return instance;
            }
        }
    }
}