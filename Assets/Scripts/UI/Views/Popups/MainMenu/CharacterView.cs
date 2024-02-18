using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private GameObject _selectedView;
        [SerializeField] private GameObject _notSelectedView;
        [SerializeField] private GameObject _characterLight;
        [SerializeField] private Button _button;
        [SerializeField] private CharacterView _otherCharacterView;
        
        [field: SerializeField] public bool IsToggled { get; protected set; }

        public event Action<bool> OnToggle;

        private void Start()
        {
            _button.onClick.AddListener(ToggleObjects);
        }

        private void ToggleObjects()
        {
            if (_otherCharacterView != null && _otherCharacterView.IsToggled)
            {
                _otherCharacterView.ToggleObjects();
            }

            IsToggled = !IsToggled;
            _characterLight.SetActive(IsToggled);
            _selectedView.SetActive(IsToggled);
            _notSelectedView.SetActive(!IsToggled);
            
            OnToggle?.Invoke(IsToggled);
        }
    }
}