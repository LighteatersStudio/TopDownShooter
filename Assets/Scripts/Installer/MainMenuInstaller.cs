using UI;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private UIBuilder _builder;
        [SerializeField] private UIRoot _menuRoot;

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
                .FromComponentInNewPrefab(_menuRoot)
                .AsSingle()
                .NonLazy();
        }
    }
}