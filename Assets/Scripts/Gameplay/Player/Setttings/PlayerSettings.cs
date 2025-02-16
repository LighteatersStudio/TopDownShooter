using System;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "LightEaters/Player/Create PlayerSettings", fileName = "PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject, IPlayerSettings 
    {
        [SerializeField] private StatsInfo _stats;
        [SerializeField] private GameObject _model;
        [SerializeField] private WeaponSettings _defaultWeapon;

        public StatsInfo Stats => _stats;
        public GameObject ModelPrefab => _model;
        public IWeaponSettings DefaultWeapon => _defaultWeapon;
    }
}