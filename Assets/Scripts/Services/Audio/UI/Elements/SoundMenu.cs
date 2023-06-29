namespace Services.Audio
{
    public class SoundMenu : SoundUIElement
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