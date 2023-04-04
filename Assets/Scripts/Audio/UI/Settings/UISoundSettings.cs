using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(menuName = "LightEaters/Create UISoundSettings", fileName = "UISoundSettings", order = 0)]
    public class UISoundSettings: ScriptableObject, IUISounds
    {
        [SerializeField] ExtendedAudioClip _buttonClick;
        [SerializeField] private ExtendedAudioClip _toggleClick;
        [SerializeField] private ExtendedAudioClip _openMenu;
        [SerializeField] private ExtendedAudioClip _closeMenu;
        [SerializeField] private ExtendedAudioClip _showMessage;

        
        

        public IAudioClip ButtonClick => _buttonClick;
        public IAudioClip ToggleClick => _toggleClick;
        public IAudioClip OpenMenu => _openMenu;
        public IAudioClip CloseMenu => _closeMenu;
        public IAudioClip ShowMessage => _showMessage;
    }
}