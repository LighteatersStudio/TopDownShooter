using Services.Level;
using Services.Loading;
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
            
            Container.Bind<LoadingService>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<MainMenuLoadingOperation>()
                .FromNew()
                .AsSingle()
                .Lazy();
            
            Container.Bind<LevelLoadingOperation>()
                .FromNew()
                .AsSingle()
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