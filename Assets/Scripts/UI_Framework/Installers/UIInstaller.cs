using UI.Framework;
using UnityEngine;
using Zenject;

namespace UI.Framework.Implementation
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private UIBuilder _globalUIBuilder;
        [SerializeField] private string _scopeName;
        
        public override void InstallBindings()
        {
            Container.Bind<UIBuilder>()
                .FromComponentInNewPrefab(_globalUIBuilder)
                .WithGameObjectName($"UIBuilder[{_scopeName}]")
                .AsSingle()
                .NonLazy();

            Container.Bind<IUIRoot>()
                .FromComponentInNewPrefab(_uiRoot)
                .WithGameObjectName($"UIRoot[{_scopeName}]")
                .AsSingle()
                .NonLazy();
        }
    }
}