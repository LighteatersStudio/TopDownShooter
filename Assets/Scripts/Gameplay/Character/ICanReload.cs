using System;
using Gameplay.Services.GameTime;

namespace Gameplay
{
    public interface ICanReload
    {
        void Reload();
        event Action<ICooldown> Reloaded;
    }
}
