using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Gameplay.Services.Input
{
    public class InputController : IInputController, IInitializable, IDisposable
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
        public event Action<Vector2> MovementFingerDown;
        public event Action<Vector2> MovementFingerMoved;
        public event Action MovementFingerUp;
        public event Action<Vector2> LookFingerDown;
        public event Action<Vector2> LookFingerMoved;
        public event Action LookFingerUp;
        public event Action<bool> FireChanged;
        public event Action<Vector2> SpecialChanged;
        public event Action MeleeChanged;
        public event Action UseChanged;
        public event Action ReloadChanged;

        [Inject]
        public InputController(InputActionAsset inputActionAsset)
        {
            _inputActionAsset = inputActionAsset;
        }

        public void Initialize()
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

        private void OnMove(InputAction.CallbackContext context)
        {
            MoveChanged?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            
            LookChanged?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnFingerDown(ETouch.Finger finger)
        {
            var isMovement = finger.screenPosition.x <= Screen.width / ScreenSizeModifier;

            if (isMovement)
            {
                _movementFinger = finger;
                _movementJoystickAnchoredPosition = ClampTouchPosition(finger.screenPosition, true);
                MovementFingerDown?.Invoke(_movementJoystickAnchoredPosition);
            }
            else
            {
                _lookFinger = finger;
                _lookJoystickAnchoredPosition = ClampTouchPosition(finger.screenPosition, false);
                LookFingerDown?.Invoke(_lookJoystickAnchoredPosition);
                FireChanged?.Invoke(true);
            }
        }

        private Vector2 ClampTouchPosition(Vector2 startPosition, bool isMovement)
        {
            var xBoundary = isMovement
                ? _joystickSize.x / JoystickSizeModifier
                : Screen.width - _joystickSize.x / JoystickSizeModifier;
            var yBoundary = _joystickSize.y / JoystickSizeModifier;
            var yUpperBoundary = Screen.height - _joystickSize.y / JoystickSizeModifier;

            startPosition.x = isMovement ? Mathf.Max(startPosition.x, xBoundary) : Mathf.Min(startPosition.x, xBoundary);
            startPosition.y = Mathf.Clamp(startPosition.y, yBoundary, yUpperBoundary);

            return startPosition;
        }

        private void OnFingerMove(ETouch.Finger finger)
        {
            if (finger == _movementFinger)
            {
                HandleMovementFinger(finger);
            }
            else if (finger == _lookFinger)
            {
                HandleLookFinger(finger);
            }
        }

        private void HandleMovementFinger(ETouch.Finger finger)
        {
            var maxMovement = _joystickSize.x / JoystickSizeModifier;
            var currentTouch = finger.currentTouch;

            var distance = Vector2.Distance(currentTouch.screenPosition, _movementJoystickAnchoredPosition);
            
            if (distance > maxMovement)
            {
                _movementKnobAnchoredPosition = (currentTouch.screenPosition - _movementJoystickAnchoredPosition).normalized * maxMovement;
            }
            else
            {
                _movementKnobAnchoredPosition = currentTouch.screenPosition - _movementJoystickAnchoredPosition;
            }
            
            var projectedKnobPosition = _movementKnobAnchoredPosition / maxMovement * _joystickSize / KnobPositionModifier;
            
            _movementAmount = _movementKnobAnchoredPosition / maxMovement;

            MoveChanged?.Invoke(_movementAmount);
            MovementFingerMoved?.Invoke(projectedKnobPosition);
        }

        private void HandleLookFinger(ETouch.Finger finger)
        {
            var maxMovement = _joystickSize.x / JoystickSizeModifier;
            var currentTouch = finger.currentTouch;

            var distance = Vector2.Distance(currentTouch.screenPosition, _lookJoystickAnchoredPosition);
            
            if (distance > maxMovement)
            {
                _lookKnobAnchoredPosition = (currentTouch.screenPosition - _lookJoystickAnchoredPosition).normalized * maxMovement;
            }
            else
            {
                _lookKnobAnchoredPosition = currentTouch.screenPosition - _lookJoystickAnchoredPosition;
            }
            
            var projectedKnobPosition = _lookKnobAnchoredPosition / maxMovement * _joystickSize / KnobPositionModifier;

            _lookAmount = _lookKnobAnchoredPosition / maxMovement;

            LookChanged?.Invoke(_lookAmount);
            LookFingerMoved?.Invoke(projectedKnobPosition);
            FireChanged?.Invoke(true);
        }

        private void OnFingerUp(ETouch.Finger finger)
        {
            if (finger == _movementFinger)
            {
                ResetMovementFinger();
            }
            else if (finger == _lookFinger)
            {
                ResetLookFinger();
            }
        }

        private void ResetMovementFinger()
        {
            _movementFinger = null;
            _movementJoystickAnchoredPosition = Vector2.zero;
            _movementAmount = Vector2.zero;
            MovementFingerUp?.Invoke();
            MoveChanged?.Invoke(Vector2.zero);
        }

        private void ResetLookFinger()
        {
            _lookFinger = null;
            _lookJoystickAnchoredPosition = Vector2.zero;
            LookFingerUp?.Invoke();
            FireChanged?.Invoke(false);
        }

        private void OnFireStart(InputAction.CallbackContext context)
        {
            if (_lookFinger != null)
            {
                return;
            }
            
            FireChanged?.Invoke(true);
        }
        
        private void OnFireEnds(InputAction.CallbackContext context)
        {
            if (_lookFinger != null)
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

        public void Dispose()
        {
            ETouch.EnhancedTouchSupport.Disable();

            ETouch.Touch.onFingerDown -= OnFingerDown;
            ETouch.Touch.onFingerMove -= OnFingerMove;
            ETouch.Touch.onFingerUp -= OnFingerUp;

            _inputActionAsset.FindAction(MoveActionName).performed -= OnMove;
            _inputActionAsset.FindAction(MoveActionName).canceled -= OnMove;
            
            _inputActionAsset.FindAction(FireActionName).performed -= OnFireStart;
            _inputActionAsset.FindAction(FireActionName).canceled -= OnFireEnds;
            
            _inputActionAsset.FindAction(LookActionName).performed -= OnLook;
            _inputActionAsset.FindAction(SpecialActionName).performed -= OnSpecial;
            _inputActionAsset.FindAction(UseActionName).performed -= OnUse;
            _inputActionAsset.FindAction(ReloadActionName).performed -= OnReload;
            _inputActionAsset.FindAction(MeleeActionName).performed -= OnMelee;
        }
    }
}