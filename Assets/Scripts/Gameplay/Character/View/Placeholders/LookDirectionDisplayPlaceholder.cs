using UnityEngine;
using Services.Utility;

namespace Gameplay.View
{
    public class LookDirectionDisplayPlaceholder: Placeholder<LookDirectionDisplay.Factory>
    {
        protected override GameObject Create(LookDirectionDisplay.Factory factory)
        {
            return factory.Create().gameObject;
        }
    }
}