using UnityEngine;
using Zenject;

namespace UI.Framework.Implementation
{
    internal class WindowSystemFactory
    {
        private readonly DiContainer _diContainer;

        public WindowSystemFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public IWindowSystem Create(GameObject prefab, string name, int order)
        {
            var system = _diContainer.InstantiatePrefab(prefab).GetComponent<IWindowSystem>();
            system.SetName($"[{order}].{name}");
            system.SetOrder(order);

            return system;
        }
    }
}