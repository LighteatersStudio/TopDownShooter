using Gameplay;
using Meta.Level;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameRunContextInstaller: MonoInstaller
    {
        [SerializeField] private PlayerCharactersSettings _playerSettings;
        [SerializeField] private GameRunSettings _gameRunSettings;

        public override void InstallBindings()
        {
            Container.Bind<SelectCharacterService>().AsSingle();
            BindPlayerCharactersSettings();
            BindGameRunContext();
        }

        private void BindPlayerCharactersSettings()
        {
            Container.Bind<IPlayerCharactersSettings>()
                .To<PlayerCharactersSettings>()
                .FromScriptableObject(_playerSettings)
                .AsSingle();
        }

        private void BindGameRunContext()
        {
            Container.Bind<GameRunSettings>()
                .FromScriptableObject(_gameRunSettings)
                .AsSingle()
                .Lazy();
        }
    }
}