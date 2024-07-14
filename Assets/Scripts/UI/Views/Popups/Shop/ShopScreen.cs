using Infrastructure.Loading;
using Meta.Level;
using Services.Loading;
using TMPro;
using UI.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Shop
{
    public class ShopScreen : Popup
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_Text _currentLevelCounter;

        private ILoadingService _loadingService;
        private ArenaLoadingOperation.Factory _arenaLoading;
        private GameRunProvider _gameRunProvider;

        [Inject]
        public void Construct(ILoadingService loadingService, ArenaLoadingOperation.Factory arenaLoading, GameRunProvider gameRunProvider)
        {
            _loadingService = loadingService;
            _arenaLoading = arenaLoading;
            _gameRunProvider = gameRunProvider;
        }

        private void Start()
        {
            _playButton.onClick.AddListener(OnPlayClicked);

            if (_gameRunProvider.GameRun != null)
            {
                _currentLevelCounter.text = _gameRunProvider.GameRun.CurrentLevel.ToString();
            }
            else
            {
                Debug.LogError("GameRunProvider.GameRun is not initialised");
            }
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