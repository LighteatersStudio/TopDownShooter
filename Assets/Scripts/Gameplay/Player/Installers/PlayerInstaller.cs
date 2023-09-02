using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [Inject] private readonly IPlayerSettings _playerSettings;

        public override void InstallBindings()
        {
            Container.Bind<ICharacterSettings>()
                .FromInstance(_playerSettings)
                .AsSingle();

            Container.Bind<Player>()
                .FromInstance(_player)
                .AsSingle();
        }
    }
}