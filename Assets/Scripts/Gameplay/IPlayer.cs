using UnityEngine;
using System;
namespace Gameplay
{
    public interface IPlayer
    {
        public event Action Dead; 

        void SetPosition(Vector3 position);
    }
}