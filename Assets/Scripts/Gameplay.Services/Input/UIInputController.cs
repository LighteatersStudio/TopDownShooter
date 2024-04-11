using System;
using UnityEngine.InputSystem;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.Input
{
    public class UIInputController : IUIInputController, IInitializable, IDisposable
    {
        private const string CancelActionName = "Cancel";
        private const string ClickActionName = "Click";
        private const string PointActionName = "Point";

        private readonly InputActionAsset _inputActionAsset;

        public event Action CancelChanged;
        public event Action ClickChanged;
        public event Action<Vector2> PointChanged;

        public UIInputController(InputActionAsset inputActionAsset)
        {
            _inputActionAsset = inputActionAsset;
        }

        private void OnCancel(InputAction.CallbackContext obj)
        {
            CancelChanged?.Invoke();
        }

        private void OnClick(InputAction.CallbackContext position)
        {
            ClickChanged?.Invoke();
        }

        private void OnPoint(InputAction.CallbackContext position)
        {
            PointChanged?.Invoke(position.ReadValue<Vector2>());
        }

        public void Initialize()
        {
            _inputActionAsset.FindAction(CancelActionName).performed += OnCancel;
            _inputActionAsset.FindAction(ClickActionName).performed += OnClick;
            _inputActionAsset.FindAction(PointActionName).performed += OnPoint;
        }

        public void Dispose()
        {
            _inputActionAsset.FindAction(CancelActionName).performed -= OnCancel;
            _inputActionAsset.FindAction(ClickActionName).performed -= OnClick;
            _inputActionAsset.FindAction(PointActionName).performed -= OnPoint;
        }
    }
}