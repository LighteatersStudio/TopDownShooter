using System;
using Gameplay;
using Meta.Level;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class CharacterSelectionScreen : Popup
    {
        [SerializeField] private CharacterView _firstCharacterView;
        [SerializeField] private CharacterView _secondCharacterView;
        [SerializeField] private Button _toBattleButton;

        private GameRunProvider _gameRun;
        private SelectCharacterModule _selectCharacterModule;
        private bool _isFirstOn;
        private bool _isSecondOn;
        
        [Inject]
        public void Construct(GameRunProvider gameRun, SelectCharacterModule selectCharacterModule)
        {
            _gameRun = gameRun;
            _selectCharacterModule = selectCharacterModule;
        }

        private void Start()
        {
            _toBattleButton.onClick.AddListener(ActivateHighMode);
            _firstCharacterView.OnToggle += FirstCharacterToggleHandler;
            _secondCharacterView.OnToggle += SecondCharacterToggleHandler;
        }
        
        private void ActivateHighMode()
        {
            _gameRun.Run(GameRunType.High);
            Close();
        }

        private void SyncToggleState(bool isToggled, int characterIndex)
        {
            if (characterIndex == 0)
            {
                _isFirstOn = isToggled;
            }
            else if (characterIndex == 1)
            {
                _isSecondOn = isToggled;
            }
            
            _selectCharacterModule.SetPlayerSettings(characterIndex);
            _toBattleButton.gameObject.SetActive(_isFirstOn != _isSecondOn);
        }
        
        private void FirstCharacterToggleHandler(bool isToggled)
        {
            SyncToggleState(isToggled, 0);
        }

        private void SecondCharacterToggleHandler(bool isToggled)
        {
            SyncToggleState(isToggled, 1);
        }

        private void OnDestroy()
        {
            _firstCharacterView.OnToggle -= FirstCharacterToggleHandler;
            _secondCharacterView.OnToggle -= SecondCharacterToggleHandler;
        }
    }
}