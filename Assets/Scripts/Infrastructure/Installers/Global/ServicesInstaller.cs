using Infrastructure.Loading;
using Services.Application.Description;
using Services.Application.Description.Implementation;
using Services.Application.Version;
using Services.Application.Version.Implementation;
using Services.Coloring;
using UnityEngine;
using UnityEngine.Serialization;
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

        [Header("ArenaList")]
        [SerializeField] private ArenaLIstSettings _arenaListSettings;

        public override void InstallBindings()
        {
            BindApplicationServices();
            BindGameColoring();
            BindArenaLoadService();
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

        private void BindArenaLoadService()
        {
            Container.Bind<ILoadArenaService>()
                .To<LoadArenaService>()
                .AsSingle()
                .NonLazy();

            Container.Bind<ArenaLIstSettings>()
                .FromInstance(_arenaListSettings)
                .AsSingle()
                .NonLazy();
        }
    }
}