using System;
using Gameplay;
using Gameplay.Services.FX;
using Gameplay.Services.GameTime;
using Gameplay.Services.Input;
using Gameplay.Services.Pause;
using Gameplay.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Infrastructure
{
    public class GameplayServicesInstaller : MonoInstaller
    {
        [Header("Input")]
        [SerializeField] private InputActionAsset _playerInputActionsMap;
        [SerializeField] private PlayerInput _playerInputPrefab;
        
        public override void InstallBindings()
        {
            BindInputController();
            BindTime();
            BindWeaponBuilder();
            BindPauseManager();
            BindFX();
        }
        
        private void BindInputController()
        {
            Debug.Log("Game installer: Bind input controller");

            Container.Bind<InputActionAsset>()
                .FromScriptableObject(_playerInputActionsMap)
                .AsSingle()
                .Lazy();

            Container.Bind<PlayerInput>()
                .FromComponentInNewPrefab(_playerInputPrefab)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IInputController>()
                .To<InputController>()
                .FromNew()
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
        
        private void BindWeaponBuilder()
        {
            Container.Bind<WeaponInitiatorDebug>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName(nameof(WeaponInitiatorDebug))
                .AsSingle()
                .NonLazy();
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
        
        private void BindFX()
        {
            Debug.Log("Game installer: Bind FX");
            Container.BindFactory<ParticleSystem, Vector3, PlayingFX, PlayingFX.Factory>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .Lazy();
        }
    }
}