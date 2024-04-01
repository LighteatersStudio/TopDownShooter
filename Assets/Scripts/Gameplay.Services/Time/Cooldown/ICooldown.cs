using System;

namespace Gameplay.Services.GameTime
{
    public interface ICooldown
    {
        float Progress { get; }
        bool IsFinish { get; }

        event Action ProgressChanged;
        event Action Completed;
    }
}