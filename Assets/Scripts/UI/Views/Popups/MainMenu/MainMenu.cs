using Services.Application.Description;
using TMPro;
using UI.Framework;
using UI.Views.Popups;
using UI.Views.Popups.CharacterSelectionMenu;
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
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
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
            _startButton.onClick.AddListener(LoadLevel);
            _settingsButton.onClick.AddListener(OpenSettings);
            _exitButton.onClick.AddListener(ApplicationExit);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(LoadLevel);
            _settingsButton.onClick.RemoveListener(OpenSettings);
            _exitButton.onClick.RemoveListener(ApplicationExit);
        }

        private void LoadLevel()
        {
            _uiRoot.Open<CharacterSelectionScreen>();
            Close();
        }

        private void OpenSettings()
        {
            Debug.Log("Settings button clicked.");
        }

        private void ApplicationExit()
        {
            Debug.Log("Application Quit.");
            Application.Quit();
        }

        public class Factory : ViewFactory<MainMenu>
        {

        }
    }
}