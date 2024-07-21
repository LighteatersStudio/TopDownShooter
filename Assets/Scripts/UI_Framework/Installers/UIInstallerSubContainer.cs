using UnityEngine;
using Zenject;

namespace UI.Framework.Implementation
{
    public class UIInstallerSubContainer : MonoInstaller
    {
        [SerializeField] private UIRoot uiRoot;
        [SerializeField] private Camera _uiCamera;
        [SerializeField] private UISchema _schema;
        [SerializeField] private string _scopeName;
        [SerializeField] private bool _useEmptyBuilderProcessor = true;
        
        public override void InstallBindings()
        {
            Container.Bind<UIBuilderInstaller>()
                .AsSingle();
            
            Container.BindFactory<ViewCreator, UIBuilder,UIBuilder.Factory>()
                .AsSingle();
            
            Container.Bind<WindowSystemFactory>()
                .AsSingle();
            
            Container.BindFactory<LayerInfo, int, Layer, Layer.Factory>();
            
            Container.Bind<UISchema>()
                .FromScriptableObject(_schema)
                .AsSingle();

            Container.Bind<IUIRoot>()
                .FromInstance(uiRoot)
                .AsSingle()
                .NonLazy();
            
            uiRoot.name = $"UIRoot[{_scopeName}]";

            if (_uiCamera)
            {
                Container.Bind<Camera>()
                    .FromComponentInNewPrefab(_uiCamera)
                    .AsSingle()
                    .Lazy();    
            }
            else
            {
                Debug.LogWarning("UI camera is not set");
            }

            if (_useEmptyBuilderProcessor)
            {
                Container.Bind<IUIBuildProcessor>()
                    .To<EmptyBuildProcessor>()
                    .AsSingle();
            }
            else
            {
                BindBuilderProcessor();
            }
        }

        protected virtual void BindBuilderProcessor()
        {

        }
    }
}