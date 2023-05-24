using Services.AppVersion;
using Services.AppVersion.Coloring;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ServicesInstaller : MonoInstaller
    {
        [Header("Version")]
        [SerializeField] private string _applicationVersion = "ApplicationVersion";
        
        [Header("GameColors")]
        [SerializeField] private ColorSchemeSettings _colorSchemeSettings;
        
        public override void InstallBindings()
        {
            BindVersion();
            BindGameColoring();
        }
        
        private void BindVersion()
        {
            Container.Bind<IApplicationVersion>()
                .To<ApplicationVersion>()
                .FromScriptableObjectResource(_applicationVersion)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindGameColoring()
        {
            Debug.Log("Global installer: Bind game coloring");

            Container.Bind<ColorSchemeSettings>()
                .FromScriptableObject(_colorSchemeSettings)
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameColoring>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}