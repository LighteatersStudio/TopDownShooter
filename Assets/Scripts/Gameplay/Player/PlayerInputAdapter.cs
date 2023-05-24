using Gameplay.Services.Input;
using Gameplay.Services.Pause;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class PlayerInputAdapter
    {
        private readonly IMovable _movingActor;
        private readonly IInputController _inputController;
        private readonly ICanFire _fireActor;
        private readonly IPause _pause;

        [Inject]
        public PlayerInputAdapter(IInputController inputController, IMovable movingActor, ICanFire fireActor, IPause pause)
        {
            _fireActor = fireActor;
            _inputController = inputController;
            _movingActor = movingActor;
            _pause = pause;
            
            Subscribe();
        }
        
        private void Subscribe()
        {
            _inputController.MoveChanged += OnMoveChanged;
            _inputController.LookChanged += OnLookChanged;
            
            _inputController.FireChanged += OnFireChanged;
        }

        private void OnMoveChanged(Vector2 direction)
        {
            _pause.TryInvokeIfNotPause(() => _movingActor.SetMoveForce(new Vector3(direction.x, 0, direction.y)));
        }
        
        private void OnLookChanged(Vector2 direction)
        {
            _pause.TryInvokeIfNotPause(() => _fireActor.LookDirection = new Vector3(direction.x, 0, direction.y));
        }

        private void OnFireChanged()
        {
            _pause.TryInvokeIfNotPause(() => _fireActor.Fire());
        }
        
        
        public class Factory : PlaceholderFactory<IMovable, ICanFire, PlayerInputAdapter>
        {
        }
    }
}