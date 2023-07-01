using System;

namespace Gameplay.Services.GameTime
{
    public interface ITicker
    {
        event Action<float> Tick;
    }
}