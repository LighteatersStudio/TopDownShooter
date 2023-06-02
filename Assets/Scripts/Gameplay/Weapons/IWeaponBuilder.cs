namespace Gameplay.Weapons
{
    public interface IWeaponBuilder
    {
        public IWeapon CreateWeapon(IWeaponUser user);
    }
}