using Infrastructure.Loading;
using Services.Loading;
using UI.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Shop
{
    public class ShopScreen : Popup
    {
        [SerializeField] private Button _playButton;

        private ILoadingService _loadingService;
        private ArenaLoadingOperation.Factory _arenaLoading;

        [Inject]
        public void Construct(ILoadingService loadingService, ArenaLoadingOperation.Factory arenaLoading)
        {
            _loadingService = loadingService;
            _arenaLoading = arenaLoading;
        }

        private void Start()
        {
            _playButton.onClick.AddListener(OnPlayClicked);
        }

        private async void OnPlayClicked()
        {
            await _loadingService.Load(_arenaLoading.Create());
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayClicked);
        }

        public class Factory : ViewFactory<ShopScreen>
        {
        }
    }
}