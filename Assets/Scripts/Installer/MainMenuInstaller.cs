using Scenarios;
using UI;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class MainMenuInstaller : MonoInstaller
    {
        [Header("UI")]
        [SerializeField] private UIBuilder _builder;
        [SerializeField] private UIRoot _menuRoot;

        public override void InstallBindings()
        {
            BindUI();
            BindScenario();
        }
        
        private void BindUI()
        {
            Debug.Log("Main menu installer: Bind UI");
            Container.Bind<UIBuilder>()
                .FromComponentInNewPrefab(_builder)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IUIRoot>()
                .FromComponentInNewPrefab(_menuRoot)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindScenario()
        {
            Debug.Log("Main menu installer: Bind scenario");
            
            Container.Bind<LaunchMainMenuScenario>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName(nameof(LaunchMainMenuScenario))
                .AsSingle()
                .NonLazy();
        }
    }
}