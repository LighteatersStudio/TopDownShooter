using UI.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenu : Popup
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
            _uiRoot.Open<HighStoneChooseMenu>();
            Close();
        }
    }
}