using Infrastructure.Loading;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ContentInstaller : MonoInstaller
    {
        [Header("ArenaList")]
        [SerializeField] private ArenaListSettings _arenaListSettings;

        public override void InstallBindings()
        {
            BindArenaLoadService();
        }

        private void BindArenaLoadService()
        {
            Container.Bind<ILoadArenaService>()
                .To<LoadArenaService>()
                .AsSingle()
                .NonLazy();

            Container.Bind<ArenaListSettings>()
                .FromInstance(_arenaListSettings)
                .AsSingle()
                .NonLazy();
        }
    }
}