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
        
        private DiContainer Container { get; set; }

        public void InstallBindings(DiContainer container)
        {
            Container = container;

            BindParameters();
            BindStates();
        }
        
        private void BindParameters()
        {
            Container.Bind<MovingPath>()
                .FromInstance(_patrolPath)
                .AsSingle();
        }
        
        protected virtual void BindStates()
        {
            Container.BindFactory<CancellationToken, InitAIState, InitAIState.Factory>();
            Container.BindFactory<CancellationToken, IdleAIState, IdleAIState.Factory>();
            Container.BindFactory<CancellationToken, PatrolAIState, PatrolAIState.Factory>();
            Container.BindFactory<CancellationToken, AttackingAIState, AttackingAIState.Factory>();
        }
    }
}