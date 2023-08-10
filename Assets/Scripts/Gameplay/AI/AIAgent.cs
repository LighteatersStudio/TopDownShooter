using UnityEngine;
using Zenject;

namespace Gameplay.AI
{
    public class AIAgent : MonoBehaviour
    {
        [SerializeField] private bool _debugTrace;
        
        private bool _isInitialized;
        
        private InitAIState.Factory _initStateFactory;
        private StateMachine _stateMachine;
        
        [Inject]
        public void Construct(InitAIState.Factory initStateFactory)
        {
            _initStateFactory = initStateFactory;
        }

        private void Start()
        {
            LaunchStateMachine();
            _isInitialized = true;
        }

        private void OnEnable()
        {
            if (_isInitialized)
            {
                _stateMachine?.Stop();
                LaunchStateMachine();
            }
        }

        private void LaunchStateMachine()
        {
            _stateMachine = new(_initStateFactory.Create, gameObject.name, _debugTrace);
            _stateMachine.Launch();
        }
        
        private void OnDisable()
        {
            _stateMachine?.Stop();
        }
    }
}