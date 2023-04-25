using System;

namespace Gameplay
{
    public interface IHaveHealth
    {
        float HealthRelative { get; }
        
        event Action HealthChanged;
    }
}