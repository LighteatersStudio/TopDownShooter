using Infrastructure.Scenraios;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScenario();
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