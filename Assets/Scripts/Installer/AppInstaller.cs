using Loader;
using Loading;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Installer
{
    public class AppInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private UIBuilder _globalUIBuilder;
        [SerializeField] private GameObject _eventSystem;
        
        public override void InstallBindings()
        {
            BindUI();
            BindLoadingOperation();
            BindScenes();
        }
        
        private void BindUI()
        {
            Debug.Log("Global installer: Bind UI");
            
            Container.Bind<UIBuilder>()
                .FromComponentInNewPrefab(_globalUIBuilder)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IUIRoot>()
                .FromComponentInNewPrefab(_uiRoot)
                .WithGameObjectName("GlobalUIRoot")
                .AsSingle()
                .NonLazy();
            
            Container.Bind<EventSystem>()
                .FromComponentInNewPrefab(_eventSystem)
                .AsSingle()
                .NonLazy();
        }

        private void BindLoadingOperation()
        {
            Debug.Log("Global installer: Bind loading operation");
            
            Container.Bind<MainMenuLoadingOperation>()
                .To<MainMenuLoadingOperation>()
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
    }
}