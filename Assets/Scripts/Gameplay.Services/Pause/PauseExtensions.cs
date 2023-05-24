using System;

namespace Gameplay.Services.Pause
{
    public static class PauseExtensions
    {
        public static void TryInvokeIfNotPause(this IPause pause, Action action)
        {
            if (!pause.Paused)
            {
                action?.Invoke();
            }
        }
    }
}