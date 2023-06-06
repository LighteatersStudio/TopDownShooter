namespace Gameplay.Weapons
{
    public interface IWeaponReadonly
    {
        string WeaponType { get; }
        
        IHaveAmmo Ammo { get; }
    }
}