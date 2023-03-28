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
            Container.Bind<UIBuilder>()
                .FromInstance(_builder)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<UIRoot>()
                .FromInstance(_menuRoot)
                .AsSingle()
                .NonLazy();
        }
    }
}