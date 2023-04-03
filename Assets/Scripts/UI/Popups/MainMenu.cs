using Level;
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
        private GameRunProvider _gameRun;

        [Inject]
        public void Construct(GameRunProvider gameRun)
        {
            _gameRun = gameRun;
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
            _gameRun.Run(GameRunType.High);
            Close();
        }
    }
}