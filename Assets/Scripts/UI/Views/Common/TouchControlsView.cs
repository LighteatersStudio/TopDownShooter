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
            _uiInputController.FingerDown += OnFingerDown;
            _uiInputController.FingerMoved += OnFingerMoved;
            _uiInputController.FingerUp += OnFingerUp;
        }

        private void OnFingerDown(Vector2 touchPosition, bool isMoving, bool isLooking)
        {
            SetJoystickPosition(isMoving, _leftJoystick, touchPosition);
            SetJoystickPosition(isLooking, _rightJoystick, touchPosition);
        }

        private void OnFingerMoved(Vector2 touchPosition, bool isMoving, bool isLooking)
        {
            if (float.IsNaN(touchPosition.x) || float.IsNaN(touchPosition.y))
            {
                return;
            }

            SetKnobPosition(isMoving, _leftJoystick, touchPosition);
            SetKnobPosition(isLooking, _rightJoystick, touchPosition);
        }

        private void OnFingerUp(bool isMoving, bool isLooking)
        {
            ResetJoystick(isMoving, _leftJoystick);
            ResetJoystick(isLooking, _rightJoystick);
        }

        private void SetJoystickPosition(bool condition, FloatingJoystick joystick, Vector2 position)
        {
            if (condition)
            {
                joystick.SetJoystickPosition(position, true);
            }
        }

        private void SetKnobPosition(bool condition, FloatingJoystick joystick, Vector2 position)
        {
            if (condition)
            {
                joystick.SetKnobPosition(position);
            }
        }

        private static void ResetJoystick(bool condition, FloatingJoystick joystick)
        {
            if (condition)
            {
                joystick.SetJoystickPosition(Vector2.zero, false);
            }
        }

        private void OnDestroy()
        {
            _uiInputController.FingerDown -= OnFingerDown;
            _uiInputController.FingerMoved -= OnFingerMoved;
            _uiInputController.FingerUp -= OnFingerUp;
        }

        public class Factory : ViewFactory<TouchControlsView>
        {
        }
    }
}