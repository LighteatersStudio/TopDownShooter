namespace Gameplay
{
    public class FriendOrFoeTag : IFriendOrFoeTag
    {
        public string TeamTag { get; }

        public FriendOrFoeTag(string tag)
        {
            TeamTag = tag;
        }
    }
}