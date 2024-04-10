using System;
using UnityEngine;

namespace Gameplay.Services.Input
{
    public interface IInputController
    {
        event Action<Vector2> MoveChanged;
        event Action<Vector2> LookChanged;
        event Action<Vector2, bool, bool> FingerDown;
        event Action<Vector2, bool, bool> FingerMoved; 
        event Action<bool, bool> FingerUp;
        event Action<bool> FireChanged;
        event Action<Vector2> SpecialChanged;
        event Action MeleeChanged;
        event Action UseChanged;
        event Action ReloadChanged;
    }
}
