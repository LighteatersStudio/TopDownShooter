using System;
using Gameplay.Services.FX;
using Gameplay.Services.GameTime;
using Gameplay.Services.Pause;
using UI;
using UnityEngine;
using Zenject;
using PixelCrushers.DialogueSystem;

namespace Infrastructure
{
    public class GameplayServicesInstaller : MonoInstaller
    {
        [SerializeField] private DialogueSystemController _dialogueSystemController;
        
        public override void InstallBindings()
        {
            BindTime();
            BindPause();
            BindFX();
            BindDialogueSystem();
        }

        private void BindTime()
        {
            Debug.Log("Game installer: Bind time");

            Container.Bind<IGameTime>()
                .To<GameTimer>()
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
        
        private void BindDialogueSystem()
        {
            Container.Bind<DialogueSystemController>()
                .FromComponentInNewPrefab(_dialogueSystemController)
                .AsSingle()
                .NonLazy();
        }
    }
}