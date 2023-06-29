using Zenject;

namespace Services.Audio
{
    public class AudioService : IAudioService
    {
        public bool IsMute
        {
            get => _soundSource.Source.mute;
            set => _soundSource.Source.mute = value;
        }

        private readonly SoundSource _soundSource;
        
        [Inject]
        public AudioService(SoundSource soundSource)
        {
            _soundSource = soundSource;
        }
    }
}