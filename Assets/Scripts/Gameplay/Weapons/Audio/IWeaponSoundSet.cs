using Services.Audio;

namespace Gameplay.Weapons
{
    public interface IWeaponSoundSet
    {
        ExtendedAudioClip PickingUp { get; }
        ExtendedAudioClip Shot { get; }
        ExtendedAudioClip Reload { get; }
    }
}