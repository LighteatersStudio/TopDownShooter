using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenu : PopupBase
    {
        [SerializeField] private Button _playButton;

        private IUIRoot _uiRoot;

        [Inject]
        public void Construct(IUIRoot uiRoot)
        {
            _uiRoot = uiRoot;
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
            var view = _uiRoot.Open<HighStoneChooseMenu>();
            view.Closed += OnHighStoneChooseMenuClosed;
        }

        private void OnHighStoneChooseMenuClosed(IView view)
        {
            view.Closed -= OnHighStoneChooseMenuClosed;
            Close();
        }
    }

}