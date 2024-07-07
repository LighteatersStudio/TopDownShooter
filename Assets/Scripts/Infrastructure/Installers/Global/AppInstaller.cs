using Infrastructure.Loading;
using Meta.Level;
using Meta.Level.Shop;
using Services.Loading;
using Services.Loading.Implementation;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class AppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLoadingService();
            BindScenes();
            BindGameRun();
        }

        private void BindLoadingService()
        {
            Debug.Log("Global installer: Bind loading operation");

            Container.Bind<ILoadingScreen>()
                .To<LoadingScreenAdapter>()
                .AsSingle()
                .NonLazy();

            Container.Bind<ILoadingService>()
                .To<LoadingService>()
                .AsSingle()
                .NonLazy();

            Container.Bind<ILoadShopService>()
                .To<LoadShopService>()
                .AsSingle();

            Container.BindFactory<MainMenuLoadingOperation, MainMenuLoadingOperation.Factory>();
            Container.BindFactory<ArenaLoadingOperation, ArenaLoadingOperation.Factory>();
            Container.BindFactory<GameRunShopLoadingOperation, GameRunShopLoadingOperation.Factory>();
        }

        private void BindScenes()
        {
            Debug.Log("Global installer: Bind scenes");

            Container.Bind<SceneNames>()
                .FromNew()
                .AsSingle()
                .Lazy();
        }

        private void BindGameRun()
        {
            Debug.Log("Global installer: Bind game runtime");

            Container.BindFactory<GameRunParameters, GameRun, GameRun.Factory>()
                .FromNew()
                .Lazy();

            Container.Bind<GameRunProvider>()
                .FromNew()
                .AsSingle()
                .Lazy();
        }
    }
}