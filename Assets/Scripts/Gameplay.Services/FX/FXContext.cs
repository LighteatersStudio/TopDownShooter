using UnityEngine;

namespace Gameplay.Services.FX
{
    public struct FXContext
    {
        public readonly Vector3 Position;
        public readonly Vector3 Direction;

        public FXContext(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction;
        }
    }
}