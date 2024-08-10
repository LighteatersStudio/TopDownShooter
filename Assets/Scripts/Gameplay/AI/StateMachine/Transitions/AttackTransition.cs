using System.Threading;
using Zenject;

namespace Gameplay.AI
{
    public class AttackTransition : BaseTransition
    {
        private readonly ObserveArea _observeArea;
        private readonly AttackAIState.Factory _factory;

        public AttackTransition(CancellationToken token, ObserveArea observeArea, AttackAIState.Factory factory)
        : base(token)
        {
            _observeArea = observeArea;
            _factory = factory;
        }

        public override void Initialize()
        {
            _observeArea.TargetsChanged += OnTargetsChanged;
        }

        private void OnTargetsChanged()
        {
            OnActivated(ActivateState(_factory));
        }

        public override void Release()
        {
            _observeArea.TargetsChanged -= OnTargetsChanged;
        }

        public class Factory : StateTransitionFactory<AttackTransition>
        {
        }
    }
}