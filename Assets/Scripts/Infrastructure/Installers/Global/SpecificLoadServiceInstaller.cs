using Infrastructure.Loading;
using Meta.Level.Shop;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class SpecificLoadServiceInstaller : MonoInstaller
    {
        [Header("ArenaList")]
        [SerializeField] private ArenaListSettings _arenaListSettings;
        [Header("ShopSettings")]
        [SerializeField] private ShopSettings _shopSettings;

        public override void InstallBindings()
        {
            Container.Bind<ILoadArenaService>()
                .FromSubContainerResolve()
                .ByMethod(BindArenaService)
                .AsSingle();

            Container.Bind<ILoadShopService>()
                .FromSubContainerResolve()
                .ByMethod(BindShopService)
                .AsSingle();
        }

        private void BindArenaService(DiContainer container)
        {
            container.Bind<ILoadArenaService>()
                .To<LoadArenaService>()
                .AsSingle();

            container.Bind<ArenaListSettings>()
                .FromInstance(_arenaListSettings)
                .AsSingle();
        }

        private void BindShopService(DiContainer container)
        {
            container.Bind<ILoadShopService>()
                .To<LoadShopService>()
                .AsSingle();

            container.Bind<ShopSettings>()
                .FromInstance(_shopSettings)
                .AsSingle();
        }
    }
}