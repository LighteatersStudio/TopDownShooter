using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.Loading
{
    [CreateAssetMenu(fileName = "ArenaListSettings", menuName = "LightEaters/Loading/ArenaListSettings")]
    public class ArenaListSettings : ScriptableObject
    {
        [field: SerializeField] private Arena[] _arenas { get; set; }
        public IReadOnlyCollection<IArena> ArenaList
        {
            get { return _arenas.Where(x => x.Enable).ToArray(); }
        }
    }
}