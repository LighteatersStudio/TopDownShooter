namespace Gameplay.Weapons
{
    public interface ILevelWeaponSettings
    {
        float LifeTime { get; }
        WeaponSettings WeaponSetting();
    }
}