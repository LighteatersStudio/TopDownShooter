using Gameplay;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameRunContextInstaller: MonoInstaller
    {
        [SerializeField] private PlayerCharactersSettings _playerSettings;
        public override void InstallBindings()
        {
            Container.Bind<SelectCharacterService>().AsSingle();
            BindPlayerCharactersSettings();
        }
        
        private void BindPlayerCharactersSettings()
        {
            Container.Bind<IPlayerCharactersSettings>()
                .To<PlayerCharactersSettings>()
                .FromScriptableObject(_playerSettings)
                .AsSingle();
        }
    }
}