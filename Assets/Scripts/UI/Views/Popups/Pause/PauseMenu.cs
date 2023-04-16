using Services.Pause;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PauseMenu : PopupBase
    {
        [SerializeField] private Button _closeButton;
        
        private IPause _pauseManager;

        [Inject]
        public void Construct(IPause pause)
        {
            _pauseManager = pause;
        }
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked()
        {
            _pauseManager.Paused = false;
            Close();
        }
    }
}
