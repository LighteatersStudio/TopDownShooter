namespace Gameplay.AI
{
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