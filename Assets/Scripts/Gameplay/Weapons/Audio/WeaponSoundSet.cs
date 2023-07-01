using System;
using Services.Audio;
using UnityEngine;

namespace Gameplay.Weapons
{
    [Serializable]
    public class WeaponSoundSet : IWeaponSoundSet
    {
        [SerializeField] private ExtendedAudioClip _pickedUp;
        [SerializeField] private ExtendedAudioClip _shot;
        [SerializeField] private ExtendedAudioClip _reload;

        public ExtendedAudioClip PickingUp => _pickedUp;
        public ExtendedAudioClip Shot => _shot;
        public ExtendedAudioClip Reload => _reload;
    }
}