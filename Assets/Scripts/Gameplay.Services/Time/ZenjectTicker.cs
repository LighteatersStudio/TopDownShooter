using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.GameTime
{
    public class ZenjectTicker : ITickable, ITicker, IDisposable
    {
        public event Action<float> Tick;
        
        void ITickable.Tick()
        {
            Tick?.Invoke(Time.deltaTime);
        }

        public void Dispose()
        {
            ForceUnsubscribeForGC();
        }

        private void ForceUnsubscribeForGC()
        {
            Tick = null;
        }
    }
}