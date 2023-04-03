using Gameplay;
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
        [SerializeField] private GameSessionStartScenario sessionStartScenario;
        [Header("Gameplay Entities")]
        [SerializeField] private Player _playerPrefab;
        
        
        
        public override void InstallBindings()
        {
            BindUI();
            BindScenarios();
            BindPlayer();
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
                .FromComponentInNewPrefab(sessionStartScenario)
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
    }
}