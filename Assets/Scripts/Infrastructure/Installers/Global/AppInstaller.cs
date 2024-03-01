using Infrastructure.Loading;
using Meta.Level;
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
                .FromNew()
                .AsSingle()
                .NonLazy();

            Container.Bind<ILoadingService>()
                .To<LoadingService>()
                .FromNew()
                .AsSingle()
                .NonLazy();

            Container.BindFactory<MainMenuLoadingOperation, MainMenuLoadingOperation.Factory>()
                .FromNew()
                .Lazy();

            Container.BindFactory<ArenaLoadingOperation, ArenaLoadingOperation.Factory>()
                .FromNew()
                .Lazy();
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

            Container.Bind<ILevelsNavigation>()
                .To<LevelsNavigation>()
                .FromNew()
                .AsSingle().Lazy();

            Container.BindFactory<GameRunType, GameRun, GameRun.Factory>()
                .FromNew()
                .Lazy();

            Container.Bind<GameRunProvider>()
                .FromNew()
                .AsSingle()
                .Lazy();
        }
    }
}