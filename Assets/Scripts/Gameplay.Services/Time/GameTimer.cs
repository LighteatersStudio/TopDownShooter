using System;

namespace Gameplay.Services.GameTime
{
    public class GameTimer : IGameTime
    {
        private readonly Cooldown.Factory _cooldownFactory;
        private Cooldown _cooldown;

        public float Value => _cooldown.RemainingTimeS;
        public event Action Finished;


        public GameTimer(Cooldown.Factory cooldownFactory)
        {
            _cooldownFactory = cooldownFactory;
            _cooldown = _cooldownFactory.CreateFinished();
        }

        public void Start(float durationS)
        {
            _cooldown = _cooldownFactory.CreateWithCommonTicker(durationS);
            _cooldown.Completed += OnFinished;
            _cooldown.Launch();
        }

        public void Break()
        {
            _cooldown.Completed -= OnFinished;
            _cooldown.ForceFinish();
        }

        private void OnFinished()
        {
            _cooldown.Completed -= OnFinished;
            Finished?.Invoke();
        }
    }
}