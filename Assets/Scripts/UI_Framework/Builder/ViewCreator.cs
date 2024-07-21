using UnityEngine;
using Zenject;

namespace UI.Framework
{
    public class ViewCreator
    {
        private readonly DiContainer _container;

        public ViewCreator(DiContainer container)
        {
            _container = container;
        }

        public GameObject Create(GameObject prefab, Transform parent)
        {
            return _container.InstantiatePrefab(prefab, parent);
        }
    }
}