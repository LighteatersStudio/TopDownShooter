using UnityEngine;
using Zenject;

namespace Gameplay.View
{
    public sealed class HealthBarPlaceholder : MonoBehaviour
    {
        private HealthBar.Factory _factory;
        private IHaveHealth _healthOwner;
        
        [Inject]
        public void Construct(HealthBar.Factory factory, IHaveHealth healthOwner)
        {
            _factory = factory;
            _healthOwner = healthOwner;
        }

        private void Start()
        {
            _factory.Create(_healthOwner, transform);
            Destroy(this);
        }
    }
}