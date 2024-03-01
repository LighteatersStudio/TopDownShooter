namespace Meta.Level
{
    public class GameRunContext
    {
        public int CountOfFinishedArenas { get; private set; } = 0;

        public void OnNextArena()
        {
            CountOfFinishedArenas++;
        }
    }
}