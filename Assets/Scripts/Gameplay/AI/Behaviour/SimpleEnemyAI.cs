using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    [Serializable]
    public class SimpleEnemyAI : IAIBehaviourInstaller
    {
        [SerializeField] private MovingPath _patrolPath;
        [SerializeField] private TargetSearchPoint _searchTargetPoint;
        
        private DiContainer Container { get; set; }

        public void InstallBindings(DiContainer container)
        {
            Container = container;

            BindParameters();
            BindTransitions(); 
            BindStates();
        }
        
        private void BindParameters()
        {
            Container.Bind<MovingPath>()
                .FromInstance(_patrolPath)
                .AsSingle();
            
            Container.Bind<TargetSearchPoint>()
                .FromInstance(_searchTargetPoint)
                .AsSingle();
        }
        
        protected virtual void BindStates()
        {
            Container.BindFactory<CancellationToken, InitAIState, InitAIState.Factory>();
            Container.BindFactory<CancellationToken, IdleAIState, IdleAIState.Factory>();
            
            
           // Container.BindFactory<CancellationToken, PatrolAIState, PatrolAIState.Factory>();
           // Container.BindFactory<CancellationToken, AttackingAIState, AttackingAIState.Factory>();
           // Container.BindFactory<CancellationToken, PursueTargetAIState, PursueTargetAIState.Factory>();
           // Container.BindFactory<CancellationToken, SearchTargetAIState, SearchTargetAIState.Factory>();
            Container.BindFactory<CancellationToken, DeathAIState, DeathAIState.Factory>();
        }

        private void BindTransitions()
        {
            Container.BindFactory<DeathTransition, DeathTransition.Factory>();

            Container.BindFactory<IdleTransitions, IdleTransitions.Factory>();
        }
    }

    public class IdleTransitions
    {
        private DeathTransition.Factory[] _factories;

        public IdleTransitions(DeathTransition.Factory deathFactory)
        {
            _factories = new[] { deathFactory };
        }
        public IEnumerable<IStateTransition> Create()
        {
            return _factories.Select(factory => factory.Create()).ToList();
        }
        
        public class Factory : PlaceholderFactory<IdleTransitions>
        {
        }
    }
}