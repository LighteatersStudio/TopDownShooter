using System;
using UnityEngine;

namespace Gameplay.Services.Input
{
    public interface IInputController
    {
        event Action<Vector2> MoveChanged;
        event Action<Vector2> LookChanged;
        event Action<Vector2> MovementFingerDown;
        event Action<Vector2> MovementFingerMoved;
        event Action MovementFingerUp;
        event Action<Vector2> LookFingerDown;
        event Action<Vector2> LookFingerMoved;
        event Action LookFingerUp;
        event Action<bool> FireChanged;
        event Action<Vector2> SpecialChanged;
        event Action MeleeChanged;
        event Action UseChanged;
        event Action ReloadChanged;
        IInputLocker Lock();
    }
}
