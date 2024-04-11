namespace Gameplay.Weapons
{
    public interface IAvailableWeaponsSettings
    {
        float LifeTime { get; }
        WeaponSettings GetRandom();
    }
}