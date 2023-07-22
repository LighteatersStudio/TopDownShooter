using System;

namespace Gameplay.Services.GameTime
{
    public static class GameTimeExtension
    {
        public static string ConvertToString(this IGameTime time)
        {
            var timeSpan = TimeSpan.FromSeconds(time.Value);
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;
            var seconds = timeSpan.Seconds;

            var result = $"{minutes:D2}:{seconds:D2}";

            if (hours > 0)
            {
                result = string.Format("{0:D2}", hours) + result;
            }

            return result;
        }
    }
}