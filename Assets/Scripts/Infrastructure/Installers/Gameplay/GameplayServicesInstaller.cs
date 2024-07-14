using System;
using Gameplay.Services.FX;
using Gameplay.Services.GameTime;
using Gameplay.Services.Pause;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameplayServicesInstaller : MonoInstaller
    {
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
            Container.Bind(typeof(IPause), typeof(IInitializable), typeof(IDisposable))
                .To<PauseManager>()
                .AsSingle()
                .NonLazy();
            
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