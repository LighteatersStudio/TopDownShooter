using System;

namespace Gameplay
{
    public interface ICanReload
    {
        void Reload();
        event Action Reloaded;
    }
}
