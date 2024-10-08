using Common.Scenarios;
using Gameplay.Scenario.FirstCutsceneScenario;
using UnityEngine;
using Zenject;

namespace Gameplay.Cutscene
{
    public class CutsceneInstaller : MonoInstaller
    {
        [SerializeField] private FirstCutscene _firstCutscene;

        public override void InstallBindings()
        {
            Container.BindFactory<FirstCutscene, FirstCutscene.Factory>()
                .FromComponentInNewPrefab(_firstCutscene);

            Container.Bind<ScenarioPlayer>()
                .AsSingle();

            Container.BindFactory<IFirstCutsceneScenario, IFirstCutsceneScenario.Factory>()
                .To<FirstCutsceneScenario>();

            Container.Bind<FirstScenarioEvent>()
                .AsSingle()
                .NonLazy();
        }
    }
}