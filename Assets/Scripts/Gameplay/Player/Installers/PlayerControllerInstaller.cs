using System;
using Gameplay.Services.GameTime;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class PlayerControllerInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MonoTicker _ticker;

        public override void InstallBindings()
        {
            Container.Bind<Rigidbody>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container.Bind<IMovable>()
                .To<MoveBehaviour>()
                .AsSingle();

            Container.Bind<MonoTicker>()
                .FromInstance(_ticker)
                .AsSingle();

            Container.Bind(typeof(PlayerInputAdapter), typeof(IInitializable), typeof(IDisposable))
                .To<PlayerInputAdapter>()
                .AsSingle()
                .NonLazy();
        }
    }
}