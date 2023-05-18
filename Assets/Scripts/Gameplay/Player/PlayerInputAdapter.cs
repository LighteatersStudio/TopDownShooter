using Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class PlayerInputAdapter
    {
        private readonly IMovable _movingActor;
        private readonly IInputController _inputController;
        private readonly ICanFire _fireActor;

        [Inject]
        public PlayerInputAdapter(IInputController inputController, IMovable movingActor, ICanFire fireActor)
        {
            _fireActor = fireActor;
            _inputController = inputController;
            _movingActor = movingActor;
            
            Subscribe();
        }
        
        private void Subscribe()
        {
            _inputController.MoveChanged += OnMoveChanged;
            _inputController.LookChanged += OnLookChanged;
            
            _inputController.FireChanged += OnFireChanged;
            
            _inputController.MeleeChanged+= OnMeleeChanged;
        }

        private void OnMoveChanged(Vector2 direction)
        {
            _movingActor.SetMoveForce(new Vector3(direction.x, 0, direction.y));
        }
        
        private void OnLookChanged(Vector2 direction)
        {
            _fireActor.LookDirection = new Vector3(direction.x, 0, direction.y);
        }
        
        private void OnFireChanged()
        {            
            _fireActor.Fire();
        }

        private void OnMeleeChanged()
        {
            _movingActor.SetMoveForce(Vector3.zero);
        }
        
        
        public class Factory : PlaceholderFactory<IMovable, ICanFire, PlayerInputAdapter>
        {
        }
    }
}