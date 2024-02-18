using Gameplay;
using Zenject;

namespace Infrastructure
{
    public static class MetaCoreInstaller 
    {
        public static void InstallMeta(this DiContainer container)
        {
            container.Bind<SelectCharacterModule>().AsSingle();
        }
    }
}