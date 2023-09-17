namespace Gameplay
{
    public class FriendOrFoeFactory
    {
        private const string PlayerTeamTag = "Player";
        private const string EnemyTeamTag = "Enemy";
        
        public IFriendOrFoeTag CreatePlayerTeam()
        {
            return new FriendOrFoeTag(PlayerTeamTag);
        }
        public IFriendOrFoeTag CreateEnemyTeam()
        {
            return new FriendOrFoeTag(EnemyTeamTag);
        }
    }
}