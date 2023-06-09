using UnityEngine;

namespace Audio.Gameplay.Weapon
{
    [CreateAssetMenu(menuName = "LightEaters/Audio/Create WeaponSoundSettings", fileName = "WeaponSoundSettings",
        order = 0)]
    public class WeaponSoundSettings : ScriptableObject, IWeaponSounds
    {
        [SerializeField] private ExtendedAudioClip _replacement;

        public IAudioClip Replacement => _replacement;
    }
}