using System;
using Gameplay.View;
using Gameplay.Projectiles;
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
        [SerializeField] private LookDirectionDisplay _lookDirectionDisplayPrefab;
        
        [Header("Weapon settings")]
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private Projectile _projectilePrefab;

        [Header("Effects")]
        [SerializeField] private ScriptableObject _characterFXList;
        
        [Inject] private StatsInfo _statsInfo;
        [Inject] private Func<Transform, GameObject> _modelFactoryMethod;
        
        
        public override void InstallBindings()
        {
            BindInjectedParameters();
            
            BindCharacterSelf();
            
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
                .FromInstance(_statsInfo)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<Func<Transform, GameObject>>()
                .FromInstance(_modelFactoryMethod)
                .AsSingle()
                .NonLazy();
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

            Container.Bind<ICanReload>()
                .To<Character>()
                .FromResolve()
                .AsCached();

            Container.Bind<IWeaponUser>()
                .To<Character>()
                .FromResolve()
                .AsCached();
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
                .To<Weapon>()
                .FromComponentInNewPrefab(_weaponPrefab)
                .AsSingle()
                .Lazy();
            
            Container.BindFactory<FlyInfo, IAttackInfo, Projectile, Projectile.Factory>()
                .FromComponentInNewPrefab(_projectilePrefab)
                .AsSingle()
                .Lazy();
        }
        
        private void BindView()
        {
            Container.BindFactory<IHaveHealth, HealthBar, HealthBar.Factory>()
                .FromComponentInNewPrefab(_healthBarPrefab)
                .Lazy();
         
            Container.BindFactory<LookDirectionDisplay, LookDirectionDisplay.Factory>()
                .FromComponentInNewPrefab(_lookDirectionDisplayPrefab)
                .Lazy();
            
            Container.Bind<ICharacterFXList>()
                .To<CharacterFXList>()
                .FromScriptableObject(_characterFXList)
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