using Services.AppVersion.Loading;
using UnityEngine;
using Zenject;

namespace Loader
{
    public class AppLoader : MonoBehaviour
    {
        private LoadingService _loadingService;
        private MainMenuLoadingOperation _menuLoadingOperation;

        [Inject]
        public void Construct(LoadingService loadingService, MainMenuLoadingOperation menuLoadingOperation)
        {
            _loadingService = loadingService;
            _menuLoadingOperation = menuLoadingOperation;
        }

        protected void Start()
        {
            LoadGame();
        }

        private async void LoadGame()
        {
            await _loadingService.Load(_menuLoadingOperation);
        }
    }
}