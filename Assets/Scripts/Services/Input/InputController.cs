using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Services.Input
{
    public class InputController : IInputController
    {
        private const string Move = "Move";
        private const string Fire = "Fire";
        private const string Special = "Special";
        private const string Melee = "Melee";
        private const string Use = "Use";
        private const string Reload = "Reload";

        private readonly InputActionAsset _inputActionAsset;
        
        public event Action<Vector2> MoveChanged;
        public event Action<Vector2> FireChanged;
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

        private void OnFire(InputAction.CallbackContext context)
        {
            FireChanged?.Invoke(context.ReadValue<Vector2>());
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
            _inputActionAsset.FindAction(Move).performed += OnMove;
            _inputActionAsset.FindAction(Fire).performed += OnFire;
            _inputActionAsset.FindAction(Special).performed += OnSpecial;
            _inputActionAsset.FindAction(Use).performed += OnUse;
            _inputActionAsset.FindAction(Reload).performed += OnReload;
            _inputActionAsset.FindAction(Melee).performed += OnMelee;
        }
    }
}
