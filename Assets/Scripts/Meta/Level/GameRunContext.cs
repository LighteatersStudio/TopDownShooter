namespace Meta.Level
{
    public class GameRunContext
    {
        private readonly int _maxLevel;

        public int CurrentLevel { get; private set; }
        public int CharacterIndex { get; private set; }
        public bool IsFinished => _maxLevel <= CurrentLevel;

        public GameRunContext(int characterIndex, int maxLevel)
        {
            CharacterIndex = characterIndex;
            _maxLevel = maxLevel;
        }

        public bool ToNextArena()
        {
            CurrentLevel++;
            return IsFinished;
        }
    }
}