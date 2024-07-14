namespace Meta.Level
{
    public class GameRunContext
    {
        private readonly int _maxLevel;

        public int CountOfFinishedArenas { get; private set; }
        public int CharacterIndex { get; private set; }
        public bool IsFinished => _maxLevel <= CountOfFinishedArenas;

        public GameRunContext(int characterIndex, int maxLevel)
        {
            CharacterIndex = characterIndex;
            _maxLevel = maxLevel;
        }

        public bool ToNextArena()
        {
            CountOfFinishedArenas++;
            return IsFinished;
        }
    }
}