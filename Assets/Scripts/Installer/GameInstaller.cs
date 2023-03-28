using Scenarios;
using UI;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private UIBuilder _builder;
        [SerializeField] private GameLaunchScenario _launchScenario;
        public override void InstallBindings()
        {
            BindUI();
            BindScenarios();
        }
        
        private void BindUI()
        {
            Debug.Log("Game installer: Bind UI");
            Container.Bind<UIBuilder>()
                .FromInstance(_builder)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IUIRoot>()
                .FromComponentInNewPrefab(_uiRoot)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindScenarios()
        {
            Debug.Log("Game installer: Bind scenarios");
            Container.Bind<GameLaunchScenario>()
                .FromComponentInNewPrefab(_launchScenario)
                .AsSingle()
                .NonLazy();
        }
    }
}