using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Services.Input
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

        private readonly InputActionAsset _inputActionAsset;
        
        public event Action<Vector2> MoveChanged;
        public event Action<Vector2> LookChanged;
        public event Action FireChanged;
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
            MoveChanged?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            LookChanged?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            FireChanged?.Invoke();
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
            _inputActionAsset.FindAction(MoveActionName).performed += OnMove;
            _inputActionAsset.FindAction(LookActionName).performed += OnLook;
            _inputActionAsset.FindAction(FireActionName).performed += OnFire;
            _inputActionAsset.FindAction(SpecialActionName).performed += OnSpecial;
            _inputActionAsset.FindAction(UseActionName).performed += OnUse;
            _inputActionAsset.FindAction(ReloadActionName).performed += OnReload;
            _inputActionAsset.FindAction(MeleeActionName).performed += OnMelee;
        }
    }
}
