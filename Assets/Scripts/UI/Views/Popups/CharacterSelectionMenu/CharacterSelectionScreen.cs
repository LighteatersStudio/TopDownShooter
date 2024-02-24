using System.Collections.Generic;
using Gameplay;
using Meta.Level;
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
        private int _index;
        private bool _isSecondOn;

        private readonly List<int> _selectedCharacters = new();


        [Inject]
        public void Construct(GameRunProvider gameRun, SelectCharacterService selectCharacterService)
        {
            _gameRun = gameRun;
            _selectCharacterService = selectCharacterService;
        }

        private void Start()
        {
            _toBattleButton.onClick.AddListener(ActivateHighMode);

            for (int i = 0; i < _characterViews.Length; i++)
            {
                int index = i;
                _characterViews[i].Toggled += () => CharacterToggleHandler(index);
            }
        }

        private void ActivateHighMode()
        {
            _gameRun.Run(GameRunType.High);
            Close();
        }

        private void SyncToggleState(int characterIndex)
        {
            _selectCharacterService.SetPlayerSettings(characterIndex);

            ToggleSelectedCharacterIndex(characterIndex);
            SwitchCharacters(characterIndex);

            _toBattleButton.gameObject.SetActive(_selectedCharacters.Count > 0);
            _characterStatsView.Setup(_selectedCharacters.Count > 0);
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

        private void CharacterToggleHandler(int index)
        {
            SyncToggleState(index);
        }

        private void OnDestroy()
        {
            _toBattleButton.onClick.RemoveListener(ActivateHighMode);

            for (int i = 0; i < _characterViews.Length; i++)
            {
                int index = i;
                _characterViews[i].Toggled -= () => CharacterToggleHandler(index);
            }
        }
    }
}