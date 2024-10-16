﻿using UnityEngine;
using System;
using Gameplay.Weapons;

namespace Gameplay
{
    public interface IPlayer 
    {
        IWeaponOwner WeaponOwner { get; }

        IHaveHealth Health { get; }

        event Action Dead; 

        void SetPosition(Vector3 position);

        void ChangeWeapon(IWeaponSettings settings);
    }
}