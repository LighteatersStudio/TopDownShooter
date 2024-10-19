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

        [Inject]
        public void Construct(
            FirstCutsceneScenario.Factory firstCutsceneScenarioFactory,
            ScenarioPlayer scenarioPlayer,
            IGameRun gameRun)
        {
            _firstCutsceneScenarioFactory = firstCutsceneScenarioFactory;
            _scenarioPlayer = scenarioPlayer;
            _gameRun = gameRun;
        }

        private async void Start()
        {
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