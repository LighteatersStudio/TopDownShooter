using System.Threading;
using Zenject;

namespace Gameplay.AI
{
    public class AIStateFactory<TState> : PlaceholderFactory<CancellationToken, IAIState> where TState : IAIState
    {
    }
}