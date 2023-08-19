using System;

namespace Gameplay.AI
{
    public interface IObserveArea
    {
        event Action TargetsChanged;
    }
}