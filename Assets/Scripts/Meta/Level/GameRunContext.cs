namespace Meta.Level
{
    public class GameRunContext
    {
        public int CountOfFinishedArenas { get; private set; } = 0;
        public int CharacterIndex { get; set; }

        public void OnNextArena()
        {
            CountOfFinishedArenas++;
        }
    }
}