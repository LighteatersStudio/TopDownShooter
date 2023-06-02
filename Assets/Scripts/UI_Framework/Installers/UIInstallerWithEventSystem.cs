using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Framework.Implementation
{
    public class UIInstallerWithEventSystem : UIInstaller
    {
        [SerializeField] private EventSystem _eventSystem;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.Bind<EventSystem>()
                .FromComponentInNewPrefab(_eventSystem)
                .AsSingle()
                .NonLazy();
        }
    }
}