using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.Popups.CharacterSelectionMenu
{
    public class CharacterView : MonoBehaviour
    {
        private static readonly int Battlecry = Animator.StringToHash("Battlecry");
        
        [SerializeField] private GameObject _selectedView;
        [SerializeField] private GameObject _notSelectedView;
        [SerializeField] private GameObject _characterLight;
        [SerializeField] private Button _button;
        [SerializeField] private Animator _animator;
        
        [field: SerializeField] public bool IsToggled { get; private set; }

        public event Action Toggled;

        private void Start()
        {
            _button.onClick.AddListener(() => SetToggle());
        }

        public void SetToggle(bool isSilence = false)
        {
            IsToggled = !IsToggled;
            
            _animator.SetBool(Battlecry, IsToggled);
            _characterLight.SetActive(IsToggled);
            _selectedView.SetActive(IsToggled);
            _notSelectedView.SetActive(!IsToggled);

            if (!isSilence)
            {
                Toggled?.Invoke();
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(() => SetToggle());
        }
    }
}