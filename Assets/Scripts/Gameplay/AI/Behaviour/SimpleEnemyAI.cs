using System;
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
            Container.BindFactory<CancellationToken, PatrolAIState, PatrolAIState.Factory>();
            Container.BindFactory<CancellationToken, AttackAIState, AttackAIState.Factory>();
            Container.BindFactory<CancellationToken, PursueTargetAIState, PursueTargetAIState.Factory>();
            Container.BindFactory<CancellationToken, SearchTargetAIState, SearchTargetAIState.Factory>();
            Container.BindFactory<CancellationToken, DeathAIState, DeathAIState.Factory>();
        }

        private void BindTransitions()
        {
            BindTransition<DeathTransition, DeathTransition.Factory>();
            BindTransition<AttackTransition, AttackTransition.Factory>();
        }

        private void BindTransition<TTransition, TFactory>()
            where TTransition : IStateTransition
            where TFactory : PlaceholderFactory<CancellationToken, TTransition>
        {
            Container.BindFactory<CancellationToken, TTransition, TFactory>()
                .FromMethod
                (
                    (container, token)
                        => container.Instantiate<TTransition>(new object[] { token })
                );
        }
    }
}