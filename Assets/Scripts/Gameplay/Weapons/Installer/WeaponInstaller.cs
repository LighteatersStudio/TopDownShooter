using Gameplay.Projectiles;
using UnityEngine;
using Zenject;

namespace Gameplay.Weapons
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField] private Weapon _weapon;

        [Inject] private IWeaponSettings _settings;
        [Inject] private IWeaponUser _weaponUser;
        
        public override void InstallBindings()
        {
            BindInjectedParameters();
            BindWeaponSelf();
            BindProjectile();
                
            BindSound();
        }

        private void BindInjectedParameters()
        {
            Container.Bind<IWeaponSettings>()
                .FromInstance(_settings)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IWeaponUser>()
                .FromInstance(_weaponUser)
                .AsSingle()
                .NonLazy();
        }

        private void BindWeaponSelf()
        {
            Container.Bind<Weapon>()
                .FromInstance(_weapon)
                .AsCached()
                .NonLazy();
            
            Container.Bind<IWeapon>()
                .To<Weapon>()
                .FromResolve()
                .AsCached()
                .NonLazy();
        }

       private void BindProjectile()
        {
            Container.BindFactory<FlyInfo, IAttackInfo, Projectile, Projectile.Factory>()
                .FromMonoPoolableMemoryPool(pool => pool
                    .FromComponentInNewPrefab(_settings.BulletPrefab)
                    .UnderTransformGroup("ProjectilePool"));
        }

        private void BindSound()
        {
            Container.Bind<IWeaponSoundSet>()
                .FromInstance(_settings.Sounds)
                .AsSingle()
                .Lazy();
            
            Container.Bind<WeaponSound>()
                .FromNewComponentOnRoot()
                .AsSingle()
                .NonLazy();
        }
    }
}