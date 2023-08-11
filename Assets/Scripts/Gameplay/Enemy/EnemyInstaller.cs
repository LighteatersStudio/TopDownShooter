using Gameplay.AI;
using Zenject;

namespace Gameplay.Enemy
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        private readonly Character _characterPrefab;
        private readonly CharacterSettings _characterSettings;
        private readonly IAIBehaviourInstaller _iuiBehaviourInstaller;

        [Inject]
        public EnemyInstaller(Character characterPrefab, CharacterSettings characterSettings, IAIBehaviourInstaller iuiBehaviourInstaller)
        {
            _characterPrefab = characterPrefab;
            _characterSettings = characterSettings;
            _iuiBehaviourInstaller = iuiBehaviourInstaller;
        }

        public override void InstallBindings()
        {
            Container.Bind<CharacterSettings>()
                .FromInstance(_characterSettings);

            Container.Bind<IAIBehaviourInstaller>()
                .FromInstance(_iuiBehaviourInstaller);

            Container.Bind<Character>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(_characterPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}