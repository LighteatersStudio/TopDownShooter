using UnityEngine;
using Utility;
using Zenject;

namespace Gameplay.View
{
    public sealed class HealthBarPlaceholder : Placeholder<HealthBar.Factory>
    {
        private IHaveHealth _healthOwner;

        [Inject]
        public void Construct(IHaveHealth healthOwner)
        {
            _healthOwner = healthOwner;
        }

        protected override GameObject Create(HealthBar.Factory factory)
        {
            return factory.Create(_healthOwner).gameObject;
        }
    }
}