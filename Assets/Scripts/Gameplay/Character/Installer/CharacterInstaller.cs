using System;
using Gameplay.View;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private ScriptableObject _characterFXList;
        
        [Inject] private StatsInfo _statsInfo;
        [Inject] private Func<Transform, GameObject> _modelFactoryMethod;
        
        public override void InstallBindings()
        {
            BindInjectedParameters();
            BindGameRules();
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
        
        private void BindGameRules()
        {
            Container.Bind<IDamageCalculator>()
                .To<DamageCalculator>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindView()
        {
            Container.BindFactory<IHaveHealth, Transform, HealthBar, HealthBar.Factory>()
                .FromComponentInNewPrefab(_healthBarPrefab)
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