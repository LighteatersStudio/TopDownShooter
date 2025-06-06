using Gameplay.Services.Input;
using UI.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class TestSplashScreen : Popup
    {
        [SerializeField] private Button _button;
        private IUIInputController _uiInputController;

        [Inject]
        public void Construct(IUIInputController uiInputController)
        {
            _uiInputController = uiInputController;
            _uiInputController.CancelChanged += Close;
        }

        private void Awake()
        {
            _button.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            _uiInputController.CancelChanged -= Close;
            _button.onClick.RemoveListener(Close);
        }

        public class Factory : ViewFactory<TestSplashScreen>
        {
        }
    }
}