using UnityEngine;
using Services.Utility;
using Zenject;

namespace Gameplay.View
{
    public sealed class ReloadBarPlaceholder : Placeholder<ReloadBar.Factory>
    {
        private IReloaded _source;

        [Inject]
        public void Construct(IReloaded source)
        {
            _source = source;
        }

        protected override GameObject Create(ReloadBar.Factory factory)
        {
            return factory.Create(_source).gameObject;
        }
    }
}