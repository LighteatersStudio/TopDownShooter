namespace Gameplay.Weapons
{
    public interface IWeaponBuilder
    {
        public IWeapon Create(IWeaponUser weaponUser);
    }
}