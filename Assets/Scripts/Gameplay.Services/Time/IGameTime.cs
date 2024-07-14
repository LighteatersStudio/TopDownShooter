using System;

namespace Gameplay.Services.GameTime
{
    public interface IGameTime
    {
        float Value { get; }
        event Action Finished;
        void Start(float durationS);
        void Break();
    }
}