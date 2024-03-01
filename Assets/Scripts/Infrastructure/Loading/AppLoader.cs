using Meta.Level;
using Services.Loading;
using UnityEngine;
using Zenject;

namespace Infrastructure.Loading
{
    public class AppLoader : MonoBehaviour
    {
        private ILoadingService _loadingService;
        private ILevelsNavigation _levelsNavigation;

        [Inject]
        public void Construct(ILoadingService loadingService, ILevelsNavigation levelsNavigation)
        {
            _loadingService = loadingService;
            _levelsNavigation = levelsNavigation;
        }

        protected void Start()
        {
            LoadGame();
        }

        private async void LoadGame()
        {
            await _loadingService.Load(_levelsNavigation.MainMenuLoading);
        }
    }
}