using System;
using Gameplay;
using Level;
using Scenarios;
using Services.GameTime;
using Services.Pause;
using UI;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class GameInstaller : MonoInstaller
    {
        [Header("UI")]
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private UIBuilder _builder;
        
        [Header("Gameplay Entities")]
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Character _characterPrefab;
        
        
        public override void InstallBindings()
        {
            BindUI();
            BindScenarios();
            BindPlayer();
            BindGameRun();
            BindPauseManager();
            BindTime();

            BindCharacter();
        }
        
        private void BindUI()
        {
            Debug.Log("Game installer: Bind UI");
            Container.Bind<UIBuilder>()
                .FromComponentInNewPrefab(_builder)
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
        
        private void BindCharacter()
        {
            Debug.Log("Game installer: Bind character");
            
            Container.BindFactory<StatsInfo, Func<Transform, GameObject>, Character,  Character.Factory>()
                .FromComponentInNewPrefab(_characterPrefab)
                .AsSingle()
                .Lazy();
        }
    }
}