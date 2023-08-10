using Zenject;

namespace Gameplay.AI
{
    public interface IAIBehaviourInstaller
    {
        public void InstallBindings(DiContainer container);
    }
}