using System;
using Gameplay.View;
using Gameplay.Weapons;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class CharacterInstaller : MonoInstaller
    {
        [Header("Character self")]
        [SerializeField] private Character _character;
        
        [Header("View components")]
        [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private ReloadBar _reloadBarPrefab;
        [SerializeField] private LookDirectionDisplay _lookDirectionDisplayPrefab;
        
        [Header("Effects")]
        [SerializeField] private ScriptableObject _characterFXList;
        [SerializeField] private CharacterMaterialColorChangeData _colorChangeData;
        
        [Inject] private ICharacterSettings _settings;
        [Inject] private IFriendOrFoeTag _friendOrFoeTag;
        
        public override void InstallBindings()
        {
            BindInjectedParameters();
            
            BindCharacterSelf();
            BindFriendForComponent();
            BindGameRules();
            BindWeapon();
            BindView();

            if (Application.isEditor)
            {
                BindDebug();    
            }
        }

        private void BindInjectedParameters()
        {
            Container.Bind<StatsInfo>()
                .FromInstance(_settings.Stats)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<Func<Transform, GameObject>>()
                .FromInstance(_settings.ModelFactory)
                .AsSingle()
                .NonLazy();

            if (!Container.HasBinding<IFriendOrFoeTag>())
            {
                Container.Bind<IFriendOrFoeTag>()
                    .FromInstance(_friendOrFoeTag)
                    .AsCached();
            }
        }

        private void BindCharacterSelf()
        {
            Container.Bind<Character>()
                .FromInstance(_character)
                .AsCached()
                .NonLazy();
            
            Container.Bind<ICharacter>()
                .To<Character>()
                .FromResolve()
                .AsCached();
            
            Container.Bind<IHaveHealth>()
                .To<Character>()
                .FromResolve()
                .AsCached();
            
            Container.Bind<ICanFire>()
                .To<Character>()
                .FromResolve()
                .AsCached();
            
            Container.Bind<IWeaponOwner>()
                .To<Character>()
                .FromResolve()
                .AsCached();
            
            Container.Bind<ICanReload>()
                .To<Character>()
                .FromResolve()
                .AsCached();
            
            Container.Bind<IReloaded>()
                .FromMethod(()=> new WeaponOwnerReloadProxy(Container.Resolve<IWeaponOwner>()))
                .AsSingle()
                .Lazy();
        }

        private void BindFriendForComponent()
        {
            Container.Bind<FriendOrFoeComponent>()
                .FromNewComponentOnRoot()
                .AsCached()
                .NonLazy();
        }

        private void BindGameRules()
        {
            Container.Bind<IDamageCalculator>()
                .To<DamageCalculator>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindWeapon()
        {
            Container.Bind<IWeapon>()
                .FromInstance(new IWeapon.Fake())
                .AsSingle()
                .Lazy();
        }
        
        private void BindView()
        {
            Container.BindFactory<IHaveHealth, HealthBar, HealthBar.Factory>()
                .FromComponentInNewPrefab(_healthBarPrefab)
                .Lazy();
         
            Container.BindFactory<IReloaded, ReloadBar, ReloadBar.Factory>()
                .FromComponentInNewPrefab(_reloadBarPrefab)
                .Lazy();

            Container.BindFactory<LookDirectionDisplay, LookDirectionDisplay.Factory>()
                .FromComponentInNewPrefab(_lookDirectionDisplayPrefab)
                .Lazy();
            
            Container.Bind<ICharacterFXList>()
                .To<CharacterFXList>()
                .FromScriptableObject(_characterFXList)
                .AsSingle()
                .Lazy();

            Container.BindFactory<GameObject, CharacterColorFeedback, CharacterColorFeedback.Factory>()
                .AsSingle();
            
            Container.Bind<CharacterMaterialColorChangeData>()
                .FromScriptableObject(_colorChangeData)
                .AsSingle()
                .Lazy();
            
            Container.Bind<CharacterFX>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindDebug()
        {
            Container.Bind<CharacterDebug>()
                .FromNewComponentOnRoot()
                .AsSingle()
                .NonLazy();
        }
    }
}