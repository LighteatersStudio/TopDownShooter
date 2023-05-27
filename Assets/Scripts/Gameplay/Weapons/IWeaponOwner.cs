namespace Gameplay.Weapons
{
    public interface IWeaponOwner : IWeaponUser
    {
        IWeaponReadonly Weapon { get; }
    }
}