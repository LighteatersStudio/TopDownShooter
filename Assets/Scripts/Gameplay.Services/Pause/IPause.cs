using System;

namespace Gameplay.Services.Pause
{
    public interface IPause
    {
        bool Paused { get; set; }
        
        event Action<IPause> PauseChanged;
    }
}
