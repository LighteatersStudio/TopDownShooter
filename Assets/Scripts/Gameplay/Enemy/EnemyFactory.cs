using Gameplay.AI;
using Zenject;

namespace Gameplay.Enemy
{
    public class EnemyFactory : PlaceholderFactory<ICharacterSettings, IAIBehaviourInstaller, Character>
    {
    }
}