using UnityEngine;
using System;
using Gameplay.Weapons;

namespace Gameplay
{
    public interface IPlayer
    {
        event Action Dead; 

        void SetPosition(Vector3 position);
        
        IWeaponOwner WeaponOwner { get; }
    }
}