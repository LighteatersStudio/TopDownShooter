namespace Meta.Level
{
    public class GameRunParameters
    {
        public readonly GameRunType RunType;
        public readonly int CharacterIndex;

        public GameRunParameters(GameRunType runType, int characterIndex)
        {
            RunType = runType;
            CharacterIndex = characterIndex;
        }
    }
}