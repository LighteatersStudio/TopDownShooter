namespace Gameplay
{
    public interface IFriendFoeSystem
    {
        bool CheckFoes(IFriendOrFoeTag arg1, IFriendOrFoeTag arg2);
        bool CheckFriend(IFriendOrFoeTag arg1, IFriendOrFoeTag arg2);
    }
}