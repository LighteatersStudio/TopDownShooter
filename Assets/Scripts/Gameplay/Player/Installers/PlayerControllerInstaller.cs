using Gameplay.Services.GameTime;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class PlayerControllerInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private SimpleTicker _ticker;
        
        public override void InstallBindings()
        {
            Container.Bind<Rigidbody>()
                .FromInstance(_rigidbody)
                .AsSingle();
            
            Container.Bind<IMovable>()
                .To<MoveBehaviour>()
                .AsSingle();
            
            Container.Bind<ITicker>()
                .FromInstance(_ticker)
                .AsSingle();

            Container.Bind<PlayerInputAdapter>()
                .AsSingle()
                .NonLazy();
        }
    }
}