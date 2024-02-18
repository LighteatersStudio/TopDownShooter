using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Loading
{
    [CreateAssetMenu(fileName = "ArenaListSettings", menuName = "Project/ArenaListSettings")]
    public class ArenaListSettings : ScriptableObject
    {
        [field: SerializeField] private Arena[] Arenas { get; set; } = new Arena[] { };

        public IEnumerable<IArena> ArenaList
        {
            get
            {
                foreach (var arena in Arenas)
                {
                    if (arena is IArena arenaInterface)
                    {
                        yield return arenaInterface;
                    }
                }
            }
        }
    }
}