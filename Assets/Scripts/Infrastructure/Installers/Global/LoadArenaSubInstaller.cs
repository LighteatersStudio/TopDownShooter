using Infrastructure.Loading;
using Zenject;

namespace Infrastructure
{
    public class LoadArenaSubInstaller : Installer<ArenaListSettings, LoadArenaSubInstaller>
    {
        private ArenaListSettings _arenaListSettings;

        public LoadArenaSubInstaller(ArenaListSettings arenaListSettings)
        {
            _arenaListSettings = arenaListSettings;
        }

        public override void InstallBindings()
        {
            Container.Bind<ILoadArenaService>()
                .FromSubContainerResolve()
                .ByMethod(SubContainer)
                .AsSingle()
                .NonLazy();
        }

        private void SubContainer(DiContainer container)
        {
            container.Bind<ILoadArenaService>()
                .To<LoadArenaService>()
                .AsSingle()
                .NonLazy();

            container.Bind<ArenaListSettings>()
                .FromInstance(_arenaListSettings)
                .AsSingle()
                .NonLazy();
        }
    }
}