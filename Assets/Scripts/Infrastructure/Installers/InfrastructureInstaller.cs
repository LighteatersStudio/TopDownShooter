using UI.Framework;
using Zenject;

namespace Infrastructure
{
    public class InfrastructureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIBuildProcessor>()
                .To<UIAudioBuildProcessor>()
                .AsSingle()
                .NonLazy();
        }
    }
}