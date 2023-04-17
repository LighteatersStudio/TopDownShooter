using System;

namespace Services.Pause
{
    public interface IPause
    {
        bool Paused { get; set; }
        
        event Action<IPause> PauseChanged;
    }
}
