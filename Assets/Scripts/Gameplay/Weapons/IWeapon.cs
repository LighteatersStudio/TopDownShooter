namespace Gameplay.Weapons
{
    public interface IWeapon : IWeaponReadonly
    {
        bool Shot();
        void Dispose();
    }
}
