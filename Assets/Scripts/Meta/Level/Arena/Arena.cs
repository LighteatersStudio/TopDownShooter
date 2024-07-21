using System;
using UnityEngine;

namespace Infrastructure.Loading
{
    [Serializable]
    public class Arena : IArena
    {
        [field: SerializeField] public string SceneName { get; private set; }
    }
}