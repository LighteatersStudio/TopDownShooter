namespace Gameplay.Weapons
{
    public interface IWeapon : IWeaponReadonly
    {
        bool WasteBullet();

        void Reload();
    }
}
