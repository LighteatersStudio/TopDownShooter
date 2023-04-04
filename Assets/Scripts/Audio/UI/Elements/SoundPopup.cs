using UI;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(PopupBase))]
    public class SoundPopup : SoundUIElement
    {
        public void OnEnable()
        {
            if (IsActive)
            {
                Play(Sounds.OpenMenu);   
            }
        }
        public void OnDisable()
        {
            if (IsActive)
            {
                Play(Sounds.CloseMenu);
            }
        }

        public override void Construct(IAudioPlayer audioPlayer, IUISounds uiSoundSettings)
        {
            base.Construct(audioPlayer, uiSoundSettings);
            OnEnable();
        }
    }
}