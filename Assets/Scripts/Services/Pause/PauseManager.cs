using System;
using UI;

public class PauseManager : IPause
{
    private bool _paused;
    public bool Paused
    {
        get => _paused;
        set
        {
            if (_paused != value)
            {
                _paused = value;
                PauseChanged?.Invoke(this);
            }
        }
    }
        
    public event Action<IPause> PauseChanged;
}
