using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Services.Input
{
    public class InputController : IInputController
    {
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

        private void Move(InputAction.CallbackContext ctx)
        {
            MoveChanged?.Invoke(Vector2.zero);
        }

        private void Fire(InputAction.CallbackContext ctx)
        {
            FireChanged?.Invoke(Vector2.zero);
        }

        private void Special(InputAction.CallbackContext ctx)
        {
            SpecialChanged?.Invoke(Vector2.zero);
        }

        private void Melee(InputAction.CallbackContext ctx)
        {
            MeleeChanged?.Invoke();
        }

        private void Use(InputAction.CallbackContext ctx)
        {
            UseChanged?.Invoke();
        }

        private void Reload(InputAction.CallbackContext ctx)
        {
            ReloadChanged?.Invoke();
        }

        private void OnEnable()
        {
            _inputActionAsset.FindAction("Move").performed += Move;
            _inputActionAsset.FindAction("Fire").performed += Fire;
            _inputActionAsset.FindAction("Special").performed += Special;
            _inputActionAsset.FindAction("Use").performed += Use;
            _inputActionAsset.FindAction("Reload").performed += Reload;
            _inputActionAsset.FindAction("Melee").performed += Melee;
        }
    }
}
