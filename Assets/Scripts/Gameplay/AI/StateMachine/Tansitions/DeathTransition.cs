using System;
using System.Threading;
using Zenject;

namespace Gameplay.AI
{
    public class DeathTransition : IStateTransition
    {
        private readonly Character _character;
        private readonly DeathAIState.Factory _factory;

        public event Action<IAIState> Activated;
        
        public DeathTransition(Character character, DeathAIState.Factory factory)
        {
            _character = character;
            _factory = factory;
        }

        public void Initialize()
        {
            _character.Dead += OnDead;
        }

        private void OnDead()
        {
            Activated?.Invoke(_factory.Create(new CancellationToken()));
        }

        public void Release()
        {
            _character.Dead -= OnDead;
        }
        
        public class Factory : PlaceholderFactory<DeathTransition>, IStateTransitionFactory
        {
            public IStateTransition CreateTransition()
            {
                return Create();
            }
        }
    }
}
