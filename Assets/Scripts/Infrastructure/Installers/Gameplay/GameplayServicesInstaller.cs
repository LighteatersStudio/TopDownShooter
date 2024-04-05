using Gameplay.Services.FX;
using Gameplay.Services.GameTime;
using Gameplay.Services.Pause;
using Infrastructure.UI;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayServicesInstaller : MonoInstaller
    {
        [Header("Menu Entities")]
        [SerializeField] private PauseMenu _pauseMenuPrefab;
        
        public override void InstallBindings()
        {
            BindTime();
            BindPause();
            BindFX();
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

            Container.Bind<Cooldown.Factory>()
                .AsSingle()
                .Lazy();
        }
        
        private void BindPause()
        {
            Container.Bind<PauseMenu>()
                .FromComponentInNewPrefab(_pauseMenuPrefab)
                .AsSingle()
                .Lazy();
            
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
        
        private void BindFX()
        {
            Debug.Log("Game installer: Bind FX");
            Container.BindFactory<ParticleSystem, FXContext, PlayingFX, PlayingFX.Factory>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .Lazy();
        }
    }
}