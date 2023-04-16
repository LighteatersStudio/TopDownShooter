using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay
{
    public class CharacterFactory
    {
        private Character _characterPrefab;
        public CharacterFactory(Character characterPrefab)
        {
            _characterPrefab = characterPrefab;
        }
        
        public Character Create(StatsInfo statsInfo, Func<Transform, GameObject> modelPrefabFactoryMethod)
        {
            var character = Object.Instantiate(_characterPrefab);
            character.Load(statsInfo, modelPrefabFactoryMethod);
            return character;
        }
    }
}