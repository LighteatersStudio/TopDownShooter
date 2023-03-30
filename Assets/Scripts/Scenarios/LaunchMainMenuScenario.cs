using System.Collections;
using Cysharp.Threading.Tasks;
using Loading;
using UI;
using UnityEngine;
using Zenject;

namespace Scenarios
{
    public class LaunchMainMenuScenario : MonoBehaviour
    {
        private IUIRoot _uiRoot;

        private LoadingService _loadingService;
        private LevelLoadingOperation _levelLoadingOperation;
        
        [Inject]
        public void Construct(IUIRoot uiRoot, LoadingService loadingService, LevelLoadingOperation levelLoadingOperation)
        {
            _uiRoot = uiRoot;
            _loadingService = loadingService;
            _levelLoadingOperation = levelLoadingOperation;
        }


        protected void Start()
        {
            //var view = _uiRoot.Open<StartSplashScreen>();
            //_uiRoot.Open<MainMenu>();
            //view.Closed += OnViewClosed;
            WaitAndLoadLevel();
        }

        // Debug code
        private async void  WaitAndLoadLevel()
        {
            await UniTask.Delay(5000);

            await _loadingService.Load(_levelLoadingOperation);
        }

        /*private void OnSplashScreenClosed(IView view)
        {
            view.Closed -= OnSplashScreenClosed;
            _uiRoot.Open<MainMenu>();
            
            Destroy(gameObject);
        }*/
    }
}