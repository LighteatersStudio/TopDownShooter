using Gameplay.Services.GameTime;
using Meta.Level;
using UnityEngine;
using Zenject;

namespace Gameplay.Scenario.FirstCutsceneScenario
{
    public class FirstScenarioStarter : MonoBehaviour
    {
        private ScenarioPlayer _scenarioPlayer;
        private FirstCutsceneScenario.Factory _firstCutsceneScenarioFactory;
        private IGameRun _gameRun;
        private IGameTime _gameTime;

        [Inject]
        public void Construct(
            FirstCutsceneScenario.Factory firstCutsceneScenarioFactory,
            ScenarioPlayer scenarioPlayer,
            IGameRun gameRun,
            IGameTime gameTime)
        {
            _firstCutsceneScenarioFactory = firstCutsceneScenarioFactory;
            _scenarioPlayer = scenarioPlayer;
            _gameRun = gameRun;
            _gameTime = gameTime;
        }

        private async void Start()
        {
            _gameTime.Break();

            if (!IsFirstLevel())
            {
                return;
            }

            await _scenarioPlayer.Play(_firstCutsceneScenarioFactory.Create());
        }

        private bool IsFirstLevel()
        {
            return _gameRun.CurrentLevel == 0;
        }
    }
}