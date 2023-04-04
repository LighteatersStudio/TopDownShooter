namespace Audio
{
    public interface IMusicPlayer
    {
        void PlayMusic(IAudioClip track);
        void StopMusic();
    }
}