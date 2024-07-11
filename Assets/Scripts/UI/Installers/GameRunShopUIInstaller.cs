using UI.Framework;
using UI.Framework.Implementation;
using UI.Shop;
using UnityEngine;
using Zenject;

namespace UI.Installers
{
    public class GameRunShopUIInstaller : MonoInstaller
    {
        [SerializeField] private UIInstallerSubContainer _uiRoot;

        public override void InstallBindings()
        {
            BindUIRoot();
            BindViews();
        }

        private void BindUIRoot()
        {
            Container.Bind<IUIRoot>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(_uiRoot)
                .AsSingle()
                .NonLazy();
        }

        private void BindViews()
        {
            Container.Bind<ShopScreen.Factory>()
                .AsSingle();
        }
    }
}