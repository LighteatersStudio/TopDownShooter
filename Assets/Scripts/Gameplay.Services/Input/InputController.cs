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
            MoveChanged?.Invoke(_movementFinger != null ? _movementAmount : context.ReadValue<Vector2>());
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            LookChanged?.Invoke(_lookFinger != null ? _lookAmount : context.ReadValue<Vector2>());
        }

        private void OnFingerDown(ETouch.Finger finger)
        {
            var isMovement = finger.screenPosition.x <= Screen.width / ScreenSizeModifier;

            if (isMovement)
            {
                SetFingerDown(ref _movementFinger, ref _movementAmount, ref _movementJoystickAnchoredPosition, finger);
            }
    
            if (!isMovement)
            {
                SetFingerDown(ref _lookFinger, ref _lookAmount, ref _lookJoystickAnchoredPosition, finger);
                FireChanged?.Invoke(true);
            }
        }

        private void SetFingerDown(ref ETouch.Finger fingerField, ref Vector2 amount, ref Vector2 joystickAnchoredPosition, ETouch.Finger finger)
        {
            fingerField = finger;
            amount = Vector2.zero;
            joystickAnchoredPosition = ClampTouchPosition(finger.screenPosition, fingerField == _movementFinger);
            FingerDown?.Invoke(joystickAnchoredPosition, fingerField == _movementFinger, fingerField == _lookFinger);
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
            
            var projectedKnobPosition = (_lookKnobAnchoredPosition / maxMovement) * _joystickSize / KnobPositionModifier;

            _lookAmount = _lookKnobAnchoredPosition / maxMovement;
            
            FingerMoved?.Invoke(projectedKnobPosition, false, true);
            FireChanged?.Invoke(true);
        }

        private void OnFingerUp(ETouch.Finger finger)
        {
            ResetFinger(ref _movementFinger, ref _movementAmount, ref _movementKnobAnchoredPosition, finger, true, false);
            ResetFinger(ref _lookFinger, ref _lookAmount, ref _lookKnobAnchoredPosition, finger, false, true);
        }

        private void ResetFinger(ref ETouch.Finger fingerField, ref Vector2 amount, ref Vector2 knobAnchoredPosition, ETouch.Finger finger, bool isMovement, bool isLook)
        {
            if (finger == fingerField)
            {
                fingerField = null;
                knobAnchoredPosition = Vector2.zero;
                FingerUp?.Invoke(isMovement, isLook);
                
                if (isLook)
                {
                    FireChanged?.Invoke(false);
                }

                amount = Vector2.zero;
            }
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
