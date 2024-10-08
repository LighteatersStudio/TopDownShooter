using System;
using Common.Scenarios;
using Zenject;

namespace Gameplay.Scenario.FirstCutsceneScenario
{
    public class FirstScenarioEvent : IDisposable
    {
        public event Action Started;
        public event Action Ended;

        public bool IsActive { get; private set; }

        private readonly ScenarioPlayer _scenarioPlayer;
        private readonly IFirstCutsceneScenario.Factory _firstCutsceneScenarioFactory;

        [Inject]
        public FirstScenarioEvent(
            IFirstCutsceneScenario.Factory firstCutsceneScenarioFactory,
            ScenarioPlayer scenarioPlayer
        )
        {
            _firstCutsceneScenarioFactory = firstCutsceneScenarioFactory;
            _scenarioPlayer = scenarioPlayer;
        }

        public async void Start()
        {
            IsActive = true;
            Started?.Invoke();
            await _scenarioPlayer.Play(_firstCutsceneScenarioFactory.Create());
            Stop();
        }

        private void Stop()
        {
            IsActive = false;
            Ended?.Invoke();
        }

        public void Dispose()
        {
        }
    }
}