using Services.Loading;
using UnityEngine;
using Zenject;

namespace Infrastructure.Loading
{
    public class AppLoader : MonoBehaviour
    {
        private ILoadingService _loadingService;
        private MainMenuLoadingOperation.Factory _mainMenuLoadingFactory;

        [Inject]
        public void Construct(ILoadingService loadingService, MainMenuLoadingOperation.Factory mainMenuLoadingFactory)
        {
            _loadingService = loadingService;
            _mainMenuLoadingFactory = mainMenuLoadingFactory;
        }

        protected void Start()
        {
            LoadGame();
        }

        private async void LoadGame()
        {
            await _loadingService.Load(_mainMenuLoadingFactory.Create());
        }
    }
}