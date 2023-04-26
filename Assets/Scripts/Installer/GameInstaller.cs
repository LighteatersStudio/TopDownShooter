using System;
using Gameplay;
using Gameplay.View;
using Level;
using Scenarios;
using Services.GameTime;
using Services.Pause;
using Services.Input;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Installer
{
    public class GameInstaller : MonoInstaller
    {
        [Header("UI")]
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private UIBuilder _builder;
        
        [Header("Level Entities")]
        [SerializeField]private Camera _playerCamera;
        
        [Header("Gameplay Entities")]
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private HealthBar _healthBarPrefab;

        [Header("Input")]
        [SerializeField] private InputActionAsset _playerInputActionsMap;
        private InputController _playerInputController;

        public override void InstallBindings()
        {
            BindUI();
            BindScenarios();
            BindPlayer();
            BindGameRun();
            BindPauseManager();
            BindTime();

            BindCamera();
            BindCharacter();
            BindInputController();
        }
        
        private void BindUI()
        {
            Debug.Log("Game installer: Bind UI");
            Container.Bind<UIBuilder>()
                .FromComponentInNewPrefab(_builder)
                .WithGameObjectName("UIBuilder[Gameplay]")
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IUIRoot>()
                .FromComponentInNewPrefab(_uiRoot)
                .WithGameObjectName($"UIRoot[Gameplay]")
                .AsSingle()
                .NonLazy();
        }
        
        private void BindScenarios()
        {
            Debug.Log("Game installer: Bind scenarios");
            
            Container.Bind<GameSessionStartScenario>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName(nameof(GameSessionStartScenario))
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayer()
        {
            Debug.Log("Game installer: Bind player");
            
            Container.Bind<IPlayer>()
                .FromComponentInNewPrefab(_playerPrefab)
                .WithGameObjectName("Player")
                .AsSingle()
                .Lazy();
        }

        private void BindGameRun()
        {
            Debug.Log("Game installer: Bind game runtime");

            Container.Bind<IGameRun>()
                .FromMethod(GetGameRun)
                .AsSingle()
                .NonLazy();
        }
        
        private IGameRun GetGameRun()
        {
            foreach (var container in Container.ParentContainers)
            {
                var gameRunProvide = container.TryResolve<GameRunProvider>();
                if (gameRunProvide != null)
                {
                    return gameRunProvide.GameRun;
                }
            }

            Debug.LogError("Cannot resolve GameRunProvider");
            return null;
        }

        private void BindPauseManager()
        {
            Container.Bind<IPause>()
                .To<PauseManager>()
                .AsSingle()
                .Lazy();
            
            Container.Bind<PauseMenuObserver>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName(nameof(PauseMenuObserver))
                .AsSingle()
                .NonLazy();
        }
        
        private void BindTime()
        {
            Debug.Log("Game installer: Bind time");
            
            Container.Bind<IGameTime>()
                .To<GameTimer>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName(nameof(GameTimer))
                .AsSingle()
                .NonLazy();
        }
        
        private void BindCamera()
        {
            Debug.Log("Game installer: Bind camera");

            Container.Bind<Camera>()
                .FromComponentInNewPrefab(_playerCamera)
                .WithGameObjectName("MainCamera")
                .AsSingle()
                .NonLazy();
            
            Container.Bind<CameraTrackingTarget>()
                .FromMethod(()=>Container.Resolve<Camera>().GetComponent<CameraTrackingTarget>())
                .AsSingle()
                .Lazy();
            
            Container.Bind<ICameraProvider>()
                .To<CameraProvider>()
                .FromNew()
                .AsSingle()
                .Lazy();
        }
        
        private void BindCharacter()
        {
            Debug.Log("Game installer: Bind character");

            Container.BindFactory<IHaveHealth, Transform, HealthBar, HealthBar.Factory>()
                .FromComponentInNewPrefab(_healthBarPrefab)
                .Lazy();
            
            Container.BindFactory<StatsInfo, Func<Transform, GameObject>, Character,  Character.Factory>()
                .FromComponentInNewPrefab(_characterPrefab)
                .Lazy();
        }

        private void BindInputController()
        {
            Debug.Log("Game installer: Bind input controller");

            Container.Bind<InputActionAsset>()
                .FromScriptableObject(_playerInputActionsMap)
                .AsSingle()
                .Lazy();

            Container.Bind<IInputController>()
                .To<InputController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}
