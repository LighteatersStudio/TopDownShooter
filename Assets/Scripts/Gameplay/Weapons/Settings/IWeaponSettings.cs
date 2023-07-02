using System;
using Gameplay.Projectiles;
using UnityEngine;

namespace Gameplay.Weapons
{
    public interface IWeaponSettings
    {
        string Id { get; }
        
        float ShotsPerSecond { get; }
        int AmmoClipSize { get; }
        int ReloadTime { get; }

        float Damage { get; }
        TypeDamage TypeDamage { get; }

        Func<Transform, GameObject> ViewFactory { get; }
        Projectile BulletPrefab { get; }
        ParticleSystem ShotFX { get; }

        IWeaponSoundSet Sounds { get; }
    }
}