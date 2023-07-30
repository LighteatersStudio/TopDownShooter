using System;
using Gameplay;
using Gameplay.CollectableItems;
using Gameplay.Services.GameTime;
using Gameplay.Weapons;
using Meta.Level;
using Infrastructure.Scenraios;
using Infrastructure.UI;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure
{
    
    public class GameInstaller : MonoInstaller
    {
        [Header("Level Entities")]
        [SerializeField]private Camera _playerCamera;
        
        [Header("Gameplay Entities: player")]
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private PlayerSettings _playerSettings;
        
        [Header("Gameplay Entities: character")]
        [SerializeField] private Character _characterPrefab;
        
        [FormerlySerializedAs("_itemSpawner")]
        [Header("Gameplay Entities: collectables")]
        [SerializeField] private ItemsFactory _itemsFactory;
        
        [Header("Gameplay Entities: weapon")]
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private WeaponUISetting _weaponUISetting;
        
        public override void InstallBindings()
        {
            BindScenarios();
            BindPlayer();
            BindGameState();
            BindGameRun();
            BindGameStatesObserver();

            BindCamera();
            BindCharacter();
            BindWeapon();
            BindCollectables();
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

            Container.Bind<IPlayerSettings>()
                .To<PlayerSettings>()
                .FromScriptableObject(_playerSettings)
                .AsSingle()
                .Lazy();
            
            Container.BindFactory<IMovable, ICanFire, ICanReload, ITicker, PlayerInputAdapter, PlayerInputAdapter.Factory>()
                .AsSingle()
                .Lazy();
            
            Container.Bind<IPlayer>()
                .FromComponentInNewPrefab(_playerPrefab)
                .WithGameObjectName("Player")
                .AsSingle()
                .Lazy();
        }

        private void BindGameState()
        {
            Container.Bind<IGameState>()
                .To<GameStateManager>()
                .FromNew()
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

        private void BindGameStatesObserver()
        {
            Container.Bind<GameWinObserver>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerDeathObserver>()
                .FromNew()
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

            Container.BindFactory<StatsInfo, Func<Transform, GameObject>, TypeGameplayObject, Character, Character.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<CharacterInstaller>(_characterPrefab);
        }
        
        private void BindCollectables()
        {
            Debug.Log("Game installer: Bind collectables");
            
            Container.BindFactory<ItemsFactory, ItemsFactory.Factory>()
                .FromComponentInNewPrefab(_itemsFactory)
                .AsSingle()
                .Lazy();
        }
        
        private void BindWeapon()
        {
            Debug.Log("Game installer: Bind weapon");

            Container.Bind<WeaponUISetting>()
                .FromScriptableObject(_weaponUISetting)
                .AsSingle()
                .Lazy();
            
            Container.BindFactory<IWeaponSettings, IWeaponUser, Weapon, Weapon.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<WeaponInstaller>(_weaponPrefab);
        }
    }
}