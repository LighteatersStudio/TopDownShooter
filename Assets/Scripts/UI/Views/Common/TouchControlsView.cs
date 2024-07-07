using Gameplay.Services.Input;
using UI.Framework;
using UnityEngine;
using Zenject;

namespace UI.Views.Common
{
    public class TouchControlsView : View
    {
        [SerializeField] private FloatingJoystick _leftJoystick;
        [SerializeField] private FloatingJoystick _rightJoystick;

        private IInputController _uiInputController;

        [Inject]
        public void Construct(IInputController inputController)
        {
            _uiInputController = inputController;
        }

        private void Awake()
        {
            if (!_leftJoystick || !_rightJoystick)
            {
                Debug.LogError("Joysticks are not set!");
                gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            _uiInputController.MovementFingerDown += OnMovementFingerDown;
            _uiInputController.MovementFingerMoved += OnMovementFingerMoved;
            _uiInputController.MovementFingerUp += OnMovementFingerUp;
            
            _uiInputController.LookFingerDown += OnLookFingerDown;
            _uiInputController.LookFingerMoved += OnLookFingerMoved;
            _uiInputController.LookFingerUp += OnLookFingerUp;
        }

        private void OnMovementFingerDown(Vector2 touchPosition)
        {
            SetJoystickPosition(_leftJoystick, touchPosition);
        }

        private void OnMovementFingerMoved(Vector2 touchPosition)
        {
            if (float.IsNaN(touchPosition.x) || float.IsNaN(touchPosition.y))
            {
                return;
            }

            SetKnobPosition(_leftJoystick, touchPosition);
        }

        private void OnMovementFingerUp()
        {
            ResetJoystick(_leftJoystick);
        }
        
        private void OnLookFingerDown(Vector2 touchPosition)
        {
            SetJoystickPosition(_rightJoystick, touchPosition);
        }

        private void OnLookFingerMoved(Vector2 touchPosition)
        {
            if (float.IsNaN(touchPosition.x) || float.IsNaN(touchPosition.y))
            {
                return;
            }

            SetKnobPosition(_rightJoystick, touchPosition);
        }

        private void OnLookFingerUp()
        {
            ResetJoystick(_rightJoystick);
        }

        private void SetJoystickPosition(FloatingJoystick joystick, Vector2 position)
        {
            joystick.SetJoystickPosition(position, true);
        }

        private void SetKnobPosition(FloatingJoystick joystick, Vector2 position)
        {
            joystick.SetKnobPosition(position);
        }

        private void ResetJoystick(FloatingJoystick joystick)
        {
            joystick.SetJoystickPosition(Vector2.zero, false);
        }

        private void OnDestroy()
        {
            _uiInputController.MovementFingerDown -= OnMovementFingerDown;
            _uiInputController.MovementFingerMoved -= OnMovementFingerMoved;
            _uiInputController.MovementFingerUp -= OnMovementFingerUp;
            
            _uiInputController.LookFingerDown -= OnLookFingerDown;
            _uiInputController.LookFingerMoved -= OnLookFingerMoved;
            _uiInputController.LookFingerUp -= OnLookFingerUp;
        }

        public class Factory : ViewFactory<TouchControlsView>
        {
        }
    }
}