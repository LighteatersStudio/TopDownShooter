using System.Threading.Tasks;

namespace Gameplay.AI
{
    public interface IAIState
    {
        Task<StateResult> Launch();
    }
    
    public class StateResult
    {
        public IAIState NextState { get; }
        public bool IsFinished { get; }

        public StateResult(IAIState nextState, bool isFinished)
        {
            NextState = nextState;
            IsFinished = isFinished;
        }
    }
}