namespace Meta.Level
{
    public class GameRunParameters
    {
        public readonly GameRunType RunType;
        public readonly int CharacterIndex;
        public readonly int MaxLevel;

        public GameRunParameters(GameRunType runType, int characterIndex, int maxLevel)
        {
            RunType = runType;
            CharacterIndex = characterIndex;
            MaxLevel = maxLevel;
        }
    }
}