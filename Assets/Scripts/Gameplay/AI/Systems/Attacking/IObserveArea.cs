using System;
using UnityEngine;

namespace Gameplay.AI
{
    public interface IObserveArea
    {
        event Action<Transform> EnemyFound;
    }
}