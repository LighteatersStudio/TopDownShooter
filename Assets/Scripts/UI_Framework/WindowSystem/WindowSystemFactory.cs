using UnityEngine;
using Zenject;

namespace UI.Framework.Implementation
{
    internal class WindowSystemFactory
    {
        private readonly DiContainer _diContainer;
        private WindowSystemOrderObserver _windowSystemOrderObserver;

        public WindowSystemFactory(DiContainer diContainer, WindowSystemOrderObserver windowSystemOrderObserver)
        {
            _windowSystemOrderObserver = windowSystemOrderObserver;
            _diContainer = diContainer;
        }

        public IWindowSystem Create(GameObject prefab, string name, int order, int systemOrder)
        {
            var instantiatePrefab = _diContainer.InstantiatePrefab(prefab);
            var system = instantiatePrefab.GetComponent<IWindowSystem>();
            system.SetName($"[{order}].{name}");
            system.SetOrder(order);


            var systemController = instantiatePrefab.GetComponent<IWindowsSystemController>();
            _windowSystemOrderObserver.Register(systemController);
            systemController.SetSystemOrder(systemOrder);

            return system;
        }
    }
}