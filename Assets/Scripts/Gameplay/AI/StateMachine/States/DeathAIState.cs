using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Gameplay.AI
{
    public class DeathAIState: IAIState
    {
        private readonly CancellationToken _token;
        private readonly IdleAIState.Factory _idleFactory;
        private readonly NavMeshMoving _moving;
        private readonly IAIAgentStop _agentStop;
        private readonly Character _character;

        public DeathAIState(CancellationToken token, IdleAIState.Factory idleFactory,
            NavMeshMoving moving, IAIAgentStop agentStop, Character character)
        {
            _token = token;
            _idleFactory = idleFactory;
            _moving = moving;
            _agentStop = agentStop;
        }

        public async Task<StateResult> Launch()
        {
            await UniTask.Yield();
            _moving.Stop();
            _agentStop.Stop();

            return new StateResult(_idleFactory.Create(_token), true);
        }

        public class Factory : PlaceholderFactory<CancellationToken, DeathAIState>
        {
        }
    }
}