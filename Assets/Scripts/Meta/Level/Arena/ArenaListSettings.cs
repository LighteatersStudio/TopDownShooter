using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Loading
{
    [CreateAssetMenu(fileName = "ArenaListSettings", menuName = "LightEaters/Loading/ArenaListSettings")]
    public class ArenaListSettings : ScriptableObject
    {
        [field: SerializeField] private Arena[] _arenas { get; set; }
        public IReadOnlyCollection<IArena> ArenaList => _arenas;
    }
}