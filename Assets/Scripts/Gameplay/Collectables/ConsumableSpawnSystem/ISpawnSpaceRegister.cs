namespace Gameplay.Collectables.ConsumableSpawnSystem
{
    public interface ISpawnSpaceRegister
    {
        void Register(ISpawnSpace spawnSpace);
        void Unregister(ISpawnSpace spawnSpace);
    }
}