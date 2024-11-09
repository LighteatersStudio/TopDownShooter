using Gameplay.Enemy;
using Gameplay.Scenario.Cutscene;
using Gameplay.Scenario.FirstCutsceneScenario;
using UnityEngine;
using Zenject;

namespace Gameplay.Scenario.Installer
{
    public class ScenarioInstaller : MonoInstaller
    {
        [SerializeField] private FirstCutscene _firstCutscene;
        [SerializeField] private ScenarioContainer _scenarioContainer;

        public override void InstallBindings()
        {
            Common();
            FirstScenario();
        }

        private void Common()
        {
            Container.Bind<ScenarioPlayer>()
                .AsSingle();
        }

        private void FirstScenario()
        {
            Container.BindFactory<FirstCutscene, FirstCutscene.Factory>()
                .FromComponentInNewPrefab(_firstCutscene);

            Container.BindFactory<FirstCutsceneScenario.FirstCutsceneScenario, FirstCutsceneScenario.FirstCutsceneScenario.Factory>()
                .To<FirstCutsceneScenario.FirstCutsceneScenario>();

            Container.Bind<ScenarioContainer>()
                .FromInstance(_scenarioContainer)
                .AsSingle();
        }
    }
}