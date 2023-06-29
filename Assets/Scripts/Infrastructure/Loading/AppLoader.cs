using Services.Loading;
using UnityEngine;
using Zenject;

namespace Infrastructure.Loading
{
    public class AppLoader : MonoBehaviour
    {
        private ILoadingService _loadingService;
        private MainMenuLoadingOperation _menuLoadingOperation;

        [Inject]
        public void Construct(ILoadingService loadingService, MainMenuLoadingOperation menuLoadingOperation)
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