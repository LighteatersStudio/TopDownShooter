using Gameplay.AI;
using Zenject;

namespace Gameplay.Enemy
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        private readonly Character _characterPrefab;
        private readonly ICharacterSettings _characterSettings;
        private readonly IAIBehaviourInstaller _iuiBehaviourInstaller;
        private readonly FriendOrFoeFactory _friendOrFoeFactory;

        [Inject]
        public EnemyInstaller(Character characterPrefab, ICharacterSettings characterSettings, FriendOrFoeFactory friendOrFoeFactory, IAIBehaviourInstaller iuiBehaviourInstaller)
        {
            _characterPrefab = characterPrefab;
            _characterSettings = characterSettings;
            _friendOrFoeFactory = friendOrFoeFactory;
            _iuiBehaviourInstaller = iuiBehaviourInstaller;
        }

        public override void InstallBindings()
        {
            Container.Bind<IFriendOrFoeTag>()
                .FromMethod(_friendOrFoeFactory.CreateEnemyTeam)
                .AsSingle();
            
            Container.Bind<ICharacterSettings>()
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