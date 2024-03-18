using System;
using Gameplay.Services.Input;
using UI.Framework;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI
{
    public class TouchControlsView : MonoBehaviour, IView
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
            _uiInputController.MoveChanged += OnPointChanged;
        }

        private void OnPointChanged(Vector2 touchPosition)
        {
            var newPosition = new Vector2(touchPosition.x * 100, touchPosition.y * 100);
            _leftJoystickKnob.transform.localPosition = newPosition;
            _leftJoystickBase.gameObject.SetActive(true);
        }

        public event Action<IView> Closed;

        public void Open()
        {
        }

        public void Close()
        {
        }
    }
}