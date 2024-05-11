using UI.Framework;
using UI.Framework.Implementation;
using UI.Views.Common;
using UnityEngine;
using Zenject;

namespace UI.New_UI
{
    public class GameplayUIInstaller : MonoInstaller
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
            Container.Bind<Hud.Factory>()
                .AsSingle();

            Container.Bind<TouchControlsView.Factory>()
                .AsSingle();

            Container.Bind<StartLevelMenu.Factory>()
                .AsSingle();
        }
    }
}