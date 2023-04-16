using System;

namespace UI
{
    public interface IPause
    {
        bool Paused { get; set; }
        
        event Action<IPause> PauseChanged;
    }
}
