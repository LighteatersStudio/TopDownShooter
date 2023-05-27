using Services.Application.Description;
using TMPro;
using UI.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenu : Popup
    {
        [Header("Controls")]
        [SerializeField] private TMP_Text _gameNameLabel;
        [SerializeField] private Image _backImage;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        
        [Header("Settings")]
        [SerializeField] private MainMenuBackgrounds _backgrounds;
        
        private IUIRoot _uiRoot;
        private IApplicationDescription _applicationDescription;

        
        [Inject]
        public void Construct(IUIRoot uiRoot, IApplicationDescription description)
        {
            _uiRoot = uiRoot;
            _applicationDescription = description;
        }

        private void Start()
        {
            _backImage.sprite = _backgrounds.GetRandom();
            _gameNameLabel.text = _applicationDescription.Name;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(LoadLevel);
            _exitButton.onClick.AddListener(ApplicationExit);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(LoadLevel);
            _exitButton.onClick.RemoveListener(ApplicationExit);
        }

        private void LoadLevel()
        {
            _uiRoot.Open<HighStoneChooseMenu>();
            Close();
        }

        private void ApplicationExit()
        {
            Application.Quit();
        }
    }
}