using System.Threading;

namespace Gameplay.AI
{
    public class DeathTransition : BaseTransition
    {
        private readonly Character _character;
        private readonly DeathAIState.Factory _factory;

        public DeathTransition(CancellationToken token, Character character, DeathAIState.Factory factory)
            : base(token)
        {
            _character = character;
            _factory = factory;
        }

        public override void Initialize()
        {
            _character.Dead += OnDead;
        }

        private void OnDead()
        {
            OnActivated(ActivateState(_factory));
        }

        public override void Release()
        {
            _character.Dead -= OnDead;
        }

        public class Factory : StateTransitionFactory<DeathTransition>
        {
        }
    }
}