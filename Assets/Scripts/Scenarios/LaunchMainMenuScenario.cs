using UI;
using UnityEngine;
using Zenject;

namespace Scenarios
{
    public class LaunchMainMenuScenario : MonoBehaviour
    {
        private IUIRoot _uiRoot;
        
        [Inject]
        public void Construct(IUIRoot uiRoot)
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
            
            Destroy(gameObject);
        }
    }
}