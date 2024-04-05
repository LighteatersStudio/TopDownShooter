using System;
using Gameplay.Services.Input;
using UI.Framework;
using UnityEngine;
using Zenject;

namespace UI.Views.Common
{
    public class TouchControlsView : View
    {
        [SerializeField] private FloatingJoystick _leftJoystickBase;
        [SerializeField] private GameObject _leftJoystickKnob;
        [SerializeField] private FloatingJoystick _rightJoystickBase;
        [SerializeField] private GameObject _rightJoystickKnob;

        private IInputController _uiInputController;

        [Inject]
        public void Construct(IInputController inputController)
        {
            _uiInputController = inputController;
            _uiInputController.FingerDown += OnFingerDown;
            _uiInputController.FingerMoved += OnFingerMoved;
            _uiInputController.FingerUp += OnFingerUp;

            _leftJoystickBase.gameObject.SetActive(false);
            _rightJoystickBase.gameObject.SetActive(false);
        }

        private void OnFingerDown(Vector2 touchPosition, bool isMoving, bool isLooking)
        {
            if (_leftJoystickBase == null || _rightJoystickBase == null)
            {
                return;
            }

            if (isMoving)
            {
                _leftJoystickBase.transform.position = touchPosition;
                _leftJoystickBase.gameObject.SetActive(true);
            }

            if (isLooking)
            {
                _rightJoystickBase.transform.position = touchPosition;
                _rightJoystickBase.gameObject.SetActive(true);
            }
        }

        private void OnFingerMoved(Vector2 touchPosition, bool isMoving, bool isLooking)
        {
            if (_leftJoystickBase == null || _rightJoystickBase == null || float.IsNaN(touchPosition.x) ||
                float.IsNaN(touchPosition.y))
            {
                return;
            }

            if (isMoving)
            {
                _leftJoystickKnob.transform.localPosition = touchPosition;
            }

            if (isLooking)
            {
                _rightJoystickKnob.transform.localPosition = touchPosition;
            }
        }

        private void OnFingerUp(bool isMoving, bool isLooking)
        {
            if (_leftJoystickBase == null || _rightJoystickBase == null)
            {
                return;
            }

            if (isMoving)
            {
                _leftJoystickBase.gameObject.SetActive(false);
            }

            if (isLooking)
            {
                _rightJoystickBase.gameObject.SetActive(false);
            }
        }
    }
}