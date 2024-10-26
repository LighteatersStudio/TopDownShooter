using System;
using Gameplay;
using Gameplay.CollectableItems;
using Gameplay.AI;
using Gameplay.Collectables.ConsumableSpawnSystem;
using Gameplay.Enemy;
using Gameplay.Collectables.FirstAid;
using Gameplay.Weapons;
using Meta.Level;
using Infrastructure.Scenraios;
using Infrastructure.UI;
using PixelCrushers.DialogueSystem;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Level Entities")]
        [SerializeField] private Camera _playerCamera;

        [Header("Gameplay Entities: player")]
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private Player _playerPrefab;

        [Header("Gameplay Entities: character")]
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private Character _enemyPrefab;

        [Header("Gameplay Entities: collectables")]
        [SerializeField] private ConsumableSpawnSettings _consumableSpawnSettings;
        [SerializeField] private WeaponCollectable _weaponCollectable;
        [SerializeField] private FirstAidKit _firstAidKitPrefab;
        [SerializeField] private FirstAidKitSettings _firstAidKitSettings;

        [Header("Gameplay Entities: weapon")]
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private WeaponUISetting _weaponUISetting;
        [SerializeField] private LevelWeaponSettings _levelWeaponSettings;

        [Header("Gameplay Entities: outline")]
        [SerializeField] private OutlineSettings _outlineSettings;

        [Header("Gameplay Entities: session")]
        [SerializeField] private LevelSettings _levelSettings;

        [Header("Dialogue Triggers")]
        [SerializeField] private DialogueSystemTrigger[] _dialogueSystemTriggers;

        private SelectCharacterService _selectCharacterService;

        [Inject]
        public void Construct(SelectCharacterService selectCharacterService)
        {
            _selectCharacterService = selectCharacterService;
        }

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
            BindFriendOrFoeSystem();
            BindOutline();
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
                .FromScriptableObject(_selectCharacterService.GetPlayerSettings)
                .AsSingle()
                .Lazy();

            Container.Bind<IPlayer>()
                .To<Player>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(_playerPrefab)
                .AsSingle()
                .NonLazy();
        }

        private void BindGameState()
        {
            Container.Bind(typeof(IGameState), typeof(IInitializable))
                .To<GameStateManager>()
                .FromNew()
                .AsSingle();

            Container.Bind<LevelSettings>()
                .FromScriptableObject(_levelSettings)
                .AsSingle();
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

            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(PlayerDeathObserver))
                .To<PlayerDeathObserver>()
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
                .FromMethod(() => Container.Resolve<Camera>().GetComponent<CameraTrackingTarget>())
                .AsSingle();

            Container.Bind(typeof(ICameraProvider), typeof(ICinemachineBrainProvider))
                .To<CameraProvider>()
                .FromNew()
                .AsSingle();
        }

        private void BindCharacter()
        {
            Debug.Log("Game installer: Bind character");

            Container.BindFactory<ICharacterSettings, IFriendOrFoeTag, Character, Character.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<CharacterInstaller>(_characterPrefab);

            Container.BindFactory<ICharacterSettings, IAIBehaviourInstaller, Character, EnemyFactory>()
                .FromSubContainerResolve()
                .ByInstaller<EnemyInstaller>()
                .WithArguments(_enemyPrefab);
        }

        private void BindCollectables()
        {
            Debug.Log("Game installer: Bind collectables");

            Container.BindFactory<Vector3, WeaponCollectable, WeaponCollectable.Factory>()
                .FromComponentInNewPrefab(_weaponCollectable)
                .AsSingle();

            Container.BindFactory<Vector3, FirstAidKit, FirstAidKit.Factory>()
                .FromComponentInNewPrefab(_firstAidKitPrefab)
                .AsSingle();

            Container.Bind<FirstAidKitSettings>()
                .FromInstance(_firstAidKitSettings)
                .AsSingle();

            Container.Bind(typeof(ConsumableSpawnSettings))
                .To<ConsumableSpawnSettings>()
                .FromInstance(_consumableSpawnSettings)
                .AsSingle();

            Container.Bind(typeof(IInitializable))
                .To<ConsumableSpawnersInvoker>()
                .AsSingle()
                .NonLazy();

            Container.Bind(typeof(IFirstAidKitSpawner))
                .To<FirstAidKitSpawner>()
                .AsSingle()
                .NonLazy();

            Container.Bind(typeof(ISpawnSpaceRegister), typeof(IConsumableSpawnSystem))
                .To<ConsumableSpawnSystem>()
                .AsSingle();
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

            Container.Bind<ILevelWeaponSettings>()
                .To<LevelWeaponSettings>()
                .FromScriptableObject(_levelWeaponSettings)
                .AsSingle()
                .Lazy();

            Container.Bind(typeof(IWeaponSpawner))
                .To<WeaponSpawner>()
                .AsSingle()
                .NonLazy();
        }

        private void BindFriendOrFoeSystem()
        {
            Container.Bind<IFriendFoeSystem>()
                .To<CommonFriendFoeSystem>()
                .AsSingle();

            Container.Bind<FriendOrFoeFactory>()
                .AsSingle();
        }

        private void BindOutline()
        {
            Container.Bind<IOutlineSettings>()
                .To<OutlineSettings>()
                .FromScriptableObject(_outlineSettings)
                .AsSingle();
        }
    }
}