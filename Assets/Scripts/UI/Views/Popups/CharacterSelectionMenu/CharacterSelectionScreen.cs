using System;
using System.Collections.Generic;
using Gameplay;
using Meta.Level;
using UI.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views.Popups.CharacterSelectionMenu
{
    public class CharacterSelectionScreen : Popup
    {
        [SerializeField] private CharacterView[] _characterViews;
        [SerializeField] private Button _toBattleButton;
        [SerializeField] private CharacterStatsView _characterStatsView;

        private GameRunProvider _gameRun;
        private SelectCharacterService _selectCharacterService;
        private readonly List<int> _selectedCharacters = new();
        private GameRunSettings _gameRunSettings;
        private int _characterIndex;

        [Inject]
        public void Construct(GameRunProvider gameRun,
            SelectCharacterService selectCharacterService,
            GameRunSettings gameRunSettings)
        {
            _gameRun = gameRun;
            _selectCharacterService = selectCharacterService;
            _gameRunSettings = gameRunSettings;
        }

        private void Start()
        {
            _toBattleButton.onClick.AddListener(ActivateHighMode);

            foreach (var characterView in _characterViews)
            {
                characterView.Toggled += SyncToggleState;
            }
        }

        private void ActivateHighMode()
        {
            var parameters = new GameRunParameters(GameRunType.High, _characterIndex, _gameRunSettings.MaxLevel);
            _gameRun.Run(parameters);
            Close();
        }

        private void SyncToggleState(CharacterView characterView)
        {
            _characterIndex = Array.IndexOf(_characterViews, characterView);
            _selectCharacterService.SetPlayerSettings(_characterIndex);

            ToggleSelectedCharacterIndex(_characterIndex);
            SwitchCharacters(_characterIndex);

            _toBattleButton.gameObject.SetActive(_selectedCharacters.Count > 0);
            _characterStatsView.Setup(_selectedCharacters.Count > 0, _selectCharacterService.GetPlayerSettings);
        }

        private void ToggleSelectedCharacterIndex(int index)
        {
            if (_selectedCharacters.Contains(index))
            {
                _selectedCharacters.Remove(index);
            }
            else
            {
                _selectedCharacters.Add(index);
            }
        }

        private void SwitchCharacters(int currentIndex)
        {
            for (int i = 0; i < _characterViews.Length; i++)
            {
                if (i != currentIndex && _characterViews[i].IsToggled)
                {
                    _characterViews[i].SetToggle(true);

                    ToggleSelectedCharacterIndex(i);
                }
            }
        }

        private void OnDestroy()
        {
            _toBattleButton.onClick.RemoveListener(ActivateHighMode);

            foreach (var characterView in _characterViews)
            {
                characterView.Toggled -= SyncToggleState;
            }
        }

        public class Factory : ViewFactory<CharacterSelectionScreen>
        {
        }
    }
}