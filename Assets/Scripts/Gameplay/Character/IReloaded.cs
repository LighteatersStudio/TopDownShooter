using System;
using Gameplay.Services.GameTime;

namespace Gameplay
{
    public interface IReloaded
    {
        event Action<ICooldown> ReloadStarted;
    }
}