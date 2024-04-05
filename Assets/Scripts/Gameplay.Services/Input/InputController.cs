using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Gameplay.Services.Input
{
    public class InputController : IInputController
    {
        private const string MoveActionName = "Move";
        private const string LookActionName = "Look";
        private const string FireActionName = "Fire";
        private const string SpecialActionName = "Special";
        private const string MeleeActionName = "Melee";
        private const string UseActionName = "Use";
        private const string ReloadActionName = "Reload";

        private const float KnobPositionModifier = 6f;
        private const float ScreenSizeModifier = 2f;
        private const float JoystickSizeModifier = 2f;

        private readonly InputActionAsset _inputActionAsset;
        private readonly Vector2 _joystickSize = new(300, 300);

        private ETouch.Finger _movementFinger;
        private ETouch.Finger _lookFinger;

        private Vector2 _movementAmount;
        private Vector2 _movementJoystickAnchoredPosition;
        private Vector2 _movementJoystickSizeDelta;
        private Vector2 _movementKnobAnchoredPosition;
        
        private Vector2 _lookAmount;
        private Vector2 _lookJoystickAnchoredPosition;
        private Vector2 _lookJoystickSizeDelta;
        private Vector2 _lookKnobAnchoredPosition;

        public event Action<Vector2> MoveChanged;
        public event Action<Vector2> LookChanged;
        public event Action<Vector2, bool, bool> FingerDown;
        public event Action<Vector2, bool, bool> FingerMoved;
        public event Action<bool, bool> FingerUp;
        public event Action<bool> FireChanged;
        public event Action<Vector2> SpecialChanged;
        public event Action MeleeChanged;
        public event Action UseChanged;
        public event Action ReloadChanged;

        [Inject]
        public InputController(InputActionAsset inputActionAsset)
        {
            _inputActionAsset = inputActionAsset;
            OnEnable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (_movementFinger is not null)
            {
                MoveChanged?.Invoke(_movementAmount);
                return;
            }
            
            MoveChanged?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            if (_lookFinger is not null)
            {
                LookChanged?.Invoke(_lookAmount);
                return;
            }
            
            LookChanged?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnFingerDown(ETouch.Finger finger)
        {
            if (_movementFinger == null && finger.screenPosition.x <= Screen.width / ScreenSizeModifier)
            {
                _movementFinger = finger;
                _movementAmount = Vector2.zero;
                _movementJoystickAnchoredPosition = ClampMovementTouchPosition(finger.screenPosition);
                FingerDown?.Invoke(_movementJoystickAnchoredPosition, true, false);
            }
            
            if (_lookFinger == null && finger.screenPosition.x > Screen.width / ScreenSizeModifier)
            {
                _lookFinger = finger;
                _lookAmount = Vector2.zero;
                _lookJoystickAnchoredPosition = ClampLookTouchPosition(finger.screenPosition);
                FingerDown?.Invoke(_lookJoystickAnchoredPosition, false, true);
                FireChanged?.Invoke(true);
            }
        }

        private Vector2 ClampMovementTouchPosition(Vector2 startPosition)
        {
            if (startPosition.x < _joystickSize.x / JoystickSizeModifier)
            {
                startPosition.x = _joystickSize.x / JoystickSizeModifier;
            }

            if (startPosition.y < _joystickSize.y / JoystickSizeModifier)
            {
                startPosition.y = _joystickSize.y / JoystickSizeModifier;
            }
            else if (startPosition.y > Screen.height - _joystickSize.y / JoystickSizeModifier)
            {
                startPosition.y = Screen.height - _joystickSize.y / JoystickSizeModifier;
            }

            return startPosition;
        }

        private Vector2 ClampLookTouchPosition(Vector2 startPosition)
        {
            if (startPosition.x > Screen.width - _joystickSize.x / JoystickSizeModifier)
            {
                startPosition.x = Screen.width - _joystickSize.x / JoystickSizeModifier;
            }

            if (startPosition.y < _joystickSize.y / JoystickSizeModifier)
            {
                startPosition.y = _joystickSize.y / JoystickSizeModifier;
            }
            else if (startPosition.y > Screen.height - _joystickSize.y / JoystickSizeModifier)
            {
                startPosition.y = Screen.height - _joystickSize.y / JoystickSizeModifier;
            }

            return startPosition;
        }

        private void OnFingerMove(ETouch.Finger finger)
        {
            HandleMovementFinger(finger);
            HandleLookFinger(finger);
        }

        private void HandleMovementFinger(ETouch.Finger finger)
        {
            if (finger != _movementFinger)
            {
                return;
            }

            var maxMovement = _joystickSize.x / JoystickSizeModifier;
            var currentTouch = _movementFinger.currentTouch;

            var distance = Vector2.Distance(currentTouch.screenPosition, _movementJoystickAnchoredPosition);
            
            if (distance > maxMovement)
            {
                _movementKnobAnchoredPosition = (currentTouch.screenPosition - _movementJoystickAnchoredPosition).normalized * maxMovement;
            }
            else
            {
                _movementKnobAnchoredPosition = currentTouch.screenPosition - _movementJoystickAnchoredPosition;
            }
            
            var projectedKnobPosition = (_movementKnobAnchoredPosition / maxMovement) * _joystickSize / KnobPositionModifier;
            
            _movementAmount = _movementKnobAnchoredPosition / maxMovement;
            
            FingerMoved?.Invoke(projectedKnobPosition, true, false);
        }

        private void HandleLookFinger(ETouch.Finger finger)
        {
            if (finger != _lookFinger)
            {
                return;
            }

            var maxMovement = _joystickSize.x / JoystickSizeModifier;
            var currentTouch = _lookFinger.currentTouch;

            var distance = Vector2.Distance(currentTouch.screenPosition, _lookJoystickAnchoredPosition);
            
            if (distance > maxMovement)
            {
                _lookKnobAnchoredPosition = (currentTouch.screenPosition - _lookJoystickAnchoredPosition).normalized * maxMovement;
            }
            else
            {
                _lookKnobAnchoredPosition = currentTouch.screenPosition - _lookJoystickAnchoredPosition;
            }
            
            var projectedKnobPosition = (_lookKnobAnchoredPosition / maxMovement) * _joystickSize / KnobPositionModifier;

            _lookAmount = _lookKnobAnchoredPosition / maxMovement;
            
            FingerMoved?.Invoke(projectedKnobPosition, false, true);
            FireChanged?.Invoke(true);
        }

        private void OnFingerUp(ETouch.Finger finger)
        {
            if (finger == _movementFinger)
            {
                _movementFinger = null;
                _movementKnobAnchoredPosition = Vector2.zero;
                FingerUp?.Invoke(true, false);
                _movementAmount = Vector2.zero;
            }
            
            if (finger == _lookFinger)
            {
                _lookFinger = null;
                _lookKnobAnchoredPosition = Vector2.zero;
                FingerUp?.Invoke(false, true);
                FireChanged?.Invoke(false);
                _lookAmount = Vector2.zero;
            }
        }

        private void OnFireStart(InputAction.CallbackContext context)
        {
            if (_lookFinger is not null)
            {
                return;
            }
            
            FireChanged?.Invoke(true);
        }
        
        private void OnFireEnds(InputAction.CallbackContext context)
        {
            if (_lookFinger is not null)
            {
                return;
            }

            FireChanged?.Invoke(false);
        }
        
        private void OnSpecial(InputAction.CallbackContext context)
        {
            SpecialChanged?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnMelee(InputAction.CallbackContext context)
        {
            MeleeChanged?.Invoke();
        }

        private void OnUse(InputAction.CallbackContext context)
        {
            UseChanged?.Invoke();
        }

        private void OnReload(InputAction.CallbackContext context)
        {
            ReloadChanged?.Invoke();
        }

        private void OnEnable()
        {
            ETouch.EnhancedTouchSupport.Enable();

            ETouch.Touch.onFingerDown += OnFingerDown;
            ETouch.Touch.onFingerMove += OnFingerMove;
            ETouch.Touch.onFingerUp += OnFingerUp;

            _inputActionAsset.FindAction(MoveActionName).performed += OnMove;
            _inputActionAsset.FindAction(MoveActionName).canceled += OnMove;
            
            _inputActionAsset.FindAction(FireActionName).performed += OnFireStart;
            _inputActionAsset.FindAction(FireActionName).canceled += OnFireEnds;
            
            _inputActionAsset.FindAction(LookActionName).performed += OnLook;
            _inputActionAsset.FindAction(SpecialActionName).performed += OnSpecial;
            _inputActionAsset.FindAction(UseActionName).performed += OnUse;
            _inputActionAsset.FindAction(ReloadActionName).performed += OnReload;
            _inputActionAsset.FindAction(MeleeActionName).performed += OnMelee;
        }
    }
}
