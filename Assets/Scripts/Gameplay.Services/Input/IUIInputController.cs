using System;
using UnityEngine;

namespace Gameplay.Services.Input
{
    public interface IUIInputController
    {
        event Action CancelChanged;
        event Action ClickChanged;
        event Action<Vector2> PointChanged;
    }
}