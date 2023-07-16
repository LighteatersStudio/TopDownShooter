using System;

namespace Gameplay.Services.GameTime
{
    public interface ITicker
    {
        event Action<float> Tick;

        public class Fake : ITicker
        {
            public event Action<float> Tick;
        }
    }
}