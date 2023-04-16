using System;

namespace UI
{
    public interface IPause
    {
        bool Paused { get; set; }
        
        public event Action<IPause> PauseChanged;
    }
}
