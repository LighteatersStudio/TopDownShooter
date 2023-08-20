using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    [RequireComponent(typeof(Character))]
    public class AIAgentInstaller : MonoInstaller
    {
        [SerializeField] private AIAgent _aiAgent;
        [SerializeField] private NavMeshMoving _movingSystems;
        [SerializeField] private ObserveArea _observeArea;

        [Inject] private IAIBehaviourInstaller _behaviourInstaller;

        public override void InstallBindings()
        {
            BindSystems();
            BindSelf();
            
            BindStates();
        }

        private void BindSelf()
        { 
            Container.Bind<AIAgent>()
                .FromInstance(_aiAgent)
                .AsSingle();
            
            Container.Bind<ObserveArea>()
                .FromInstance(_observeArea)
                .AsSingle();
        }

        private void BindSystems()
        {
            Container.Bind<NavMeshMoving>()
                .FromInstance(_movingSystems)
                .AsSingle();
        }
        
        private void BindStates()
        {
            _behaviourInstaller.InstallBindings(Container);
        }
    }
}