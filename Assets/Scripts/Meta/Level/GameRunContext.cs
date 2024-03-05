namespace Meta.Level
{
    public class GameRunContext
    {
        public int CountOfFinishedArenas { get; private set; } = 0;
        public int CharacterIndex { get; private set; }

        public GameRunContext(int characterIndex)
        {
            CharacterIndex = characterIndex;
        }

        public void OnNextArena()
        {
            CountOfFinishedArenas++;
        }
    }
}