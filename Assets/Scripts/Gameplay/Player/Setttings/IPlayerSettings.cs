using Gameplay.Weapons;

namespace Gameplay
{
    public interface IPlayerSettings : ICharacterSettings
    {
        IWeaponSettings DefaultWeapon { get; }
    }
}