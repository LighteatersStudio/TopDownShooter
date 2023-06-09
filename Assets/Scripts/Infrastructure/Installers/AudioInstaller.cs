using Audio;
using Audio.Gameplay.Weapon;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class AudioInstaller : MonoInstaller
    {
        [Header("Audio")]
        [SerializeField] private SoundSource _soundSource;
        [SerializeField] private UISoundSettings _uiSoundSettings;
        [SerializeField] private WeaponSoundSettings _weaponSoundSettings;
        [SerializeField] private MusicList _musicList;

        public override void InstallBindings()
        {
            Debug.Log("Global installer: Bind audio");
            
            Container.Bind<SoundSource>()
                .FromComponentInNewPrefab(_soundSource)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IAudioService>()
                .To<AudioService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IAudioPlayer>()
                .To<AudioPlayer>()
                .FromNew()
                .AsCached()
                .Lazy();
            
            Container.Bind<IMusicPlayer>()
                .To<AudioPlayer>()
                .FromNew()
                .AsCached()
                .Lazy();
            
            Container.Bind<IUISounds>()
                .To<UISoundSettings>()
                .FromInstance(_uiSoundSettings)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IWeaponSounds>()
                .To<WeaponSoundSettings>()
                .FromInstance(_weaponSoundSettings)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IMusicList>()
                .FromInstance(_musicList)
                .AsSingle()
                .Lazy();
        }
    }
}