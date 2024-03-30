namespace Gameplay.Collectables.ConsumableSpawnSystem
{
    public interface ISpawnSpaceRegister
    {
        void RegisterSpawnSpace(ISpawnSpace spawnSpace);
        void UnregisterSpawnSpace(ISpawnSpace spawnSpace);
    }
}