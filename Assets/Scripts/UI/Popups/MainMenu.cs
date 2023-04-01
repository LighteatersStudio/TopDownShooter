using Loading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenu : View
    {
        [SerializeField] private Button _playButton;
        
        private IUIRoot _uiRoot;
        private LoadingService _loadingService;
        private LevelLoadingOperation _levelLoadingOperation;

        [Inject]
        public void Construct(LoadingService loadingService, LevelLoadingOperation levelLoadingOperation)
        {
            _loadingService = loadingService;
            _levelLoadingOperation = levelLoadingOperation;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(LoadLevel);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(LoadLevel);
        }

        private void LoadLevel()
        {
            _loadingService.Load(_levelLoadingOperation);
            Close();
        }
    }
}