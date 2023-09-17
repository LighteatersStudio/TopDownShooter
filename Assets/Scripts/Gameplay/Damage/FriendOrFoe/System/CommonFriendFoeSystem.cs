namespace Gameplay
{
    public class CommonFriendFoeSystem : IFriendFoeSystem
    {
        public bool CheckFoes(IFriendOrFoeTag arg1, IFriendOrFoeTag arg2)
        {
            return arg1.TeamTag != arg2.TeamTag;
        }

        public bool CheckFriend(IFriendOrFoeTag arg1, IFriendOrFoeTag arg2)
        {
            return arg1.TeamTag == arg2.TeamTag;
        }
    }
}