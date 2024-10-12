using Common.Scenarios;
using Gameplay.Scenario.FirstCutsceneScenario;
using UnityEngine;
using Zenject;

namespace Gameplay.Cutscene
{
    public class ScenarioInstaller : MonoInstaller
    {
        [SerializeField] private FirstCutscene _firstCutscene;

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

            Container.BindFactory<FirstCutsceneScenario, FirstCutsceneScenario.Factory>()
                .To<FirstCutsceneScenario>();
        }
    }
}