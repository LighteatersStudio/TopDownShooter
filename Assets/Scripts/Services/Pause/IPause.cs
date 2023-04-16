using System;

namespace UI
{
    public interface IPause
    {
        public bool Paused
        {
            get;
            set;
        }
        
        event Action<IPause> PauseChanged;

    }
}
