using Gameplay;
using Level;
using Scenarios;
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
        [Header("Scenarios")]
        [SerializeField] private GameSessionStartScenario _sessionStartScenario;
        [Header("Gameplay Entities")]
        [SerializeField] private Player _playerPrefab;
        
        public override void InstallBindings()
        {
            BindUI();
            BindScenarios();
            BindPlayer();
            BindGameRun();
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
                .FromComponentInNewPrefab(_sessionStartScenario)
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
    }
}