using Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class PlayerInputAdapter
    {
        private IMovable _actor;
        private IInputController _inputController;
            
        [Inject]
        public PlayerInputAdapter(IInputController inputController, IMovable actor)
        {
            _inputController = inputController;
            _actor = actor;
            Subscribe();
        }
        
        private void Subscribe()
        {
            _inputController.MoveChanged += OnMoveChanged;
        }

        private void OnMoveChanged(Vector2 direction)
        {
            _actor.SetMoveForce(new Vector3(direction.x, 0, direction.y));
        }
        
        public class Factory : PlaceholderFactory<IMovable, PlayerInputAdapter>
        {
        }
    }
}