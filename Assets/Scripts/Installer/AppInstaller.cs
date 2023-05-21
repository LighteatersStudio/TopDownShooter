using Audio;
using Coloring;
using Level;
using Loader;
using Loading;
using Services;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Installer
{
    public class AppInstaller : MonoInstaller
    {
        [Header("UI")]
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private UIBuilder _globalUIBuilder;
        [SerializeField] private GameObject _eventSystem;
        
        [Header("Audio")]
        [SerializeField] private SoundSource _soundSource;
        [SerializeField] private UISoundSettings _uiSoundSettings;
        [SerializeField] private MusicList _musicList;
        
        [Header("GameColors")]
        [SerializeField] private ColorSchemeSettings _colorSchemeSettings;
        
        [Header("Version")]
        [SerializeField] private string _applicationVersion = "ApplicationVersion";

        
        public override void InstallBindings()
        {
            BindVersion();
            
            BindUI();
            BindAudio();
            BindLoadingService();
            BindScenes();
            BindGameRuntime();

            BindGameColoring();
        }
        
        private void BindUI()
        {
            Debug.Log("Global installer: Bind UI");
            
            Container.Bind<UIBuilder>()
                .FromComponentInNewPrefab(_globalUIBuilder)
                .WithGameObjectName("UIBuilder[Global]")
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IUIRoot>()
                .FromComponentInNewPrefab(_uiRoot)
                .WithGameObjectName("UIRoot[Global]")
                .AsSingle()
                .NonLazy();
            
            Container.Bind<EventSystem>()
                .FromComponentInNewPrefab(_eventSystem)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<UIAudioBuilder>()
                .FromNew()
                .AsSingle()
                .Lazy();
        }

        private void BindAudio()
        {
            Debug.Log("Global installer: Bind audio");
            
            Container.Bind<SoundSource>()
                .FromComponentInNewPrefab(_soundSource)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IAudioService>()
                .To<AudioService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IAudioPlayer>()
                .To<AudioPlayer>()
                .FromNew()
                .AsCached()
                .Lazy();
            
            Container.Bind<IMusicPlayer>()
                .To<AudioPlayer>()
                .FromNew()
                .AsCached()
                .Lazy();
            
            Container.Bind<IUISounds>()
                .To<UISoundSettings>()
                .FromInstance(_uiSoundSettings)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IMusicList>()
                .FromInstance(_musicList)
                .AsSingle()
                .Lazy();
        }
        
        private void BindLoadingService()
        {
            Debug.Log("Global installer: Bind loading operation");

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
        
        private void BindGameRuntime()
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

        private void BindVersion()
        {
            Debug.Log("Global installer: Bind version");
            
            Container.Bind<IApplicationVersion>()
                .To<ApplicationVersion>()
                .FromScriptableObjectResource(_applicationVersion)
                .AsSingle()
                .NonLazy();
        }
        

        private void BindGameColoring()
        {
            Debug.Log("Global installer: Bind game coloring");

            Container.Bind<ColorSchemeSettings>()
                .FromScriptableObject(_colorSchemeSettings)
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameColoring>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}