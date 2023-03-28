using UI;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot _menuRoot;
        [SerializeField] private UIBuilder _builder;

        public override void InstallBindings()
        {
            BindUI();
        }
        
        private void BindUI()
        {
            Debug.Log("Main menu installer: Bind UI");
            Container.Bind<UIBuilder>()
                .FromInstance(_builder)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IUIRoot>()
                .FromInstance(_menuRoot)
                .AsSingle()
                .NonLazy();
        }
    }
}