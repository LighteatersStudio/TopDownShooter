namespace Services.Audio
{
    public class SoundMessage : SoundUIElement
    {
        protected void Start()
        {
            Play(Sounds.ShowMessage);
        }
    }
}