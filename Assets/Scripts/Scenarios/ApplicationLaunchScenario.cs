using UI;
using UnityEngine;
using Zenject;

namespace Scenarios
{
    public class ApplicationLaunchScenario : MonoBehaviour
    {
        private UIRoot _uiRoot;
        
        [Inject]
        public void Construct(UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }
        
        protected void Start()
        {
            //var view = _uiRoot.Open<StartSplashScreen>();
            // view.Closed += OnViewClosed;
        }
        
        private void OnSplashScreenClosed(IView view)
        {
            view.Closed -= OnSplashScreenClosed;
            // _uiRoot.Open<MainMenu>();
        }
    }
}