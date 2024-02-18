using Gameplay;
using Meta;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class AppRoot: MonoInstaller<AppRoot>
    {
        [SerializeField] private DataConfig _playerSettings;
        
        public override void InstallBindings()
        {
            BindDataConfig();
            
            Container.InstallMeta();
        }

        private void BindDataConfig()
        {
            Container.Bind<DataConfig>()
                .FromScriptableObject(_playerSettings)
                .AsSingle()
                .Lazy();
        }
    }
}