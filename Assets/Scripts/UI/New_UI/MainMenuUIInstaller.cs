using UI.Framework;
using UI.Framework.Implementation;
using UI.Views.Popups.CharacterSelectionMenu;
using UnityEngine;
using Zenject;

namespace UI.New_UI
{
    public class MainMenuUIInstaller : MonoInstaller
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
            Container.Bind<StartSplashScreen.Factory>()
                .AsSingle();

            Container.Bind<MainMenu.Factory>()
                .AsSingle();

            Container.Bind<CharacterSelectionScreen.Factory>()
                .AsSingle();
        }
    }
}