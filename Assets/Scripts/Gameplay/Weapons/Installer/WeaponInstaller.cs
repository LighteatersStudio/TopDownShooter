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