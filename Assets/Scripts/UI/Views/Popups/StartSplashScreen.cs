using Gameplay.Services.Input;
using UI.Framework;
using Zenject;

namespace UI
{
    public class StartSplashScreen : Popup
    {
        private IUIInputController _uiInputController;

        [Inject]
        public void Construct(IUIInputController uiInputController)
        {
            _uiInputController = uiInputController;
            _uiInputController.ClickChanged += Close;
        }

        private void OnDestroy()
        {
            _uiInputController.ClickChanged -= Close;
        }

        public class Factory : ViewFactory<StartSplashScreen>
        {

        }
    }
}