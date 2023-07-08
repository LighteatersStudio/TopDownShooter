using Services.Application.Description;
using Services.Application.Description.Implementation;
using Services.Application.Version;
using Services.Application.Version.Implementation;
using Services.Coloring;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ServicesInstaller : MonoInstaller
    {
        [Header("Version")]
        [SerializeField] private string _applicationVersion = "ApplicationVersion";
        [SerializeField] private string _applicationDescription = "ApplicationDescription";
        
        [Header("GameColors")]
        [SerializeField] private ColorSchemeSettings _colorSchemeSettings;
        
        public override void InstallBindings()
        {
            BindApplicationServices();
            BindGameColoring();
        }
        
        private void BindApplicationServices()
        {
            Container.Bind<IApplicationDescription>()
                .To<ApplicationDescription>()
                .FromScriptableObjectResource(_applicationDescription)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IApplicationVersion>()
                .To<ApplicationVersion>()
                .FromScriptableObjectResource(_applicationVersion)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindGameColoring()
        {
            Debug.Log("Global installer: Bind game coloring");

            Container.Bind<IColorSchemeSettings>()
                .To<ColorSchemeSettings>()
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