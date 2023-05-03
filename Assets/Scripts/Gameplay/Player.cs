using UnityEngine;
using System;

namespace Gameplay
{
    public class Player : MonoBehaviour, IPlayer
    {
        public event Action Dead;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}