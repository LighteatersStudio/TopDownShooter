using UnityEngine;
using Services.Utility;
using Zenject;

namespace Gameplay.View
{
    public sealed class ReloadBarPlaceholder : Placeholder<ReloadBar.Factory>
    {
        private ICanReload _weaponOwner;

        [Inject]
        public void Construct(ICanReload weaponOwner)
        {
            _weaponOwner = weaponOwner;
        }

        protected override GameObject Create(ReloadBar.Factory factory)
        {
            return factory.Create(_weaponOwner).gameObject;
        }
    }
}