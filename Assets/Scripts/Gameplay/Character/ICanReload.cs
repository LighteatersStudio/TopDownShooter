using System;

namespace Gameplay
{
    public interface ICanReload
    {
        float ReloadTime { get; }
        void Reload();
        event Action ReloadChanged;
    }
}
