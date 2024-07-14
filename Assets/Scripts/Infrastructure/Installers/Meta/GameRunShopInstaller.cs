using Infrastructure.Scenraios;
using UnityEngine;
using Zenject;

namespace Installer.Meta
{
    public class GameRunShopInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScenario();
        }

        private void BindScenario()
        {
            Debug.Log("Main menu installer: Bind scenario");

            Container.Bind<LaunchShopScenario>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName(nameof(LaunchShopScenario))
                .AsSingle()
                .NonLazy();
        }
    }
}