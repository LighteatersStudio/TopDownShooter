using System.Collections.Generic;

namespace Gameplay
{
    public interface IPlayerCharactersSettings
    {
        IEnumerable<PlayerSettings> PlayerSettingsArray { get; }
    }
}