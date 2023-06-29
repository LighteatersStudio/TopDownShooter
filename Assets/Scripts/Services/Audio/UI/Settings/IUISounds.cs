namespace Services.Audio
{
    public interface IUISounds
    {
        IAudioClip ButtonClick { get; }
        IAudioClip ToggleClick { get; }
        IAudioClip OpenMenu { get; }
        IAudioClip CloseMenu { get; }
        IAudioClip ShowMessage { get; }
        IAudioClip ButtonHover { get; }
    }
}