using System;
using Gameplay.Projectiles;
using UnityEngine;

namespace Gameplay.Weapons
{
    [CreateAssetMenu(menuName = "LightEaters/Weapon/Create WeaponSettings", fileName = "WeaponSettings", order = 0)]
    public class WeaponSettings : ScriptableObject, IWeaponSettings
    {
        [Header("Game ID")]
        [SerializeField] private string _id;
        
        [Header("Shooting settings")]
        [SerializeField] private float _shotsPerSecond = 2f;
        [SerializeField] private int _ammoClipSize = 50;
        [SerializeField] private int _reloadTime = 1;

        [Header("Damage settings")] 
        [SerializeField] private float _damage = 1f;
        [SerializeField] private TypeDamage _typeDamage = TypeDamage.Fire;
        
        [Header("Prefabs")]
        [SerializeField] private GameObject _viewPrefab;
        [SerializeField] private Projectile _bulletPrefab;
        
        [Header("FX")]
        [SerializeField] private ParticleSystem _shotFX;

        [Header("Sounds")] 
        [SerializeField] private WeaponSoundSet _sounds;

        public string Id => _id;
        public float ShotsPerSecond => _shotsPerSecond;
        public int AmmoClipSize => _ammoClipSize;
        public int ReloadTime => _reloadTime;
        public float Damage => _damage;
        public TypeDamage TypeDamage => _typeDamage;
        public Projectile BulletPrefab => _bulletPrefab;
        public ParticleSystem ShotFX => _shotFX;
        public Func<Transform, GameObject> ViewFactory => CreateView;

        public IWeaponSoundSet Sounds => _sounds;
        
        private GameObject CreateView(Transform parent)
        {
            return Instantiate(_viewPrefab, parent);
        }
    }
}