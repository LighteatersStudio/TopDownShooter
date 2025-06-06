using Gameplay.Enemy;
using System.Threading.Tasks;
using Gameplay.Scenario.Cutscene;
using Gameplay.Services.Input;
using System;
using Zenject;

namespace Gameplay.Scenario.FirstCutsceneScenario
{
    public class FirstCutsceneScenario : IScenario
    {
        private readonly FirstCutscene.Factory _firstCutsceneFactory;
        private readonly IInputController _inputService;
        private readonly EnemyFactory _enemyFactory;
        private readonly ScenarioContainer _scenarioContainer;

        [Inject]
        public FirstCutsceneScenario (
            FirstCutscene.Factory firstCutsceneFactory,
            IInputController inputService,
            EnemyFactory enemyFactory,
            ScenarioContainer scenarioContainer)
        {
            _firstCutsceneFactory = firstCutsceneFactory;
            _inputService = inputService;
            _enemyFactory = enemyFactory;
            _scenarioContainer = scenarioContainer;
        }

        public async Task Launch()
        {
            using (_inputService.Lock())
            {
                await _firstCutsceneFactory.Create().Play();
                await Task.Delay(TimeSpan.FromSeconds(5f));

                var enemy = _enemyFactory.Create(_scenarioContainer.EnemySettings, _scenarioContainer.EnemySettings.SimpleEnemyAI);
                enemy.transform.SetPositionAndRotation(_scenarioContainer.SpawnEnemyPoint.position, _scenarioContainer.SpawnEnemyPoint.rotation);
            }
        }

        public class Factory : PlaceholderFactory<FirstCutsceneScenario>
        {
        }
    }
}