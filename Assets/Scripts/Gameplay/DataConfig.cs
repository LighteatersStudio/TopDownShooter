using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "LightEaters/Meta/Create DataConfig", fileName = "DataConfig")]
    public class DataConfig: ScriptableObject
    {
        [field: SerializeField] public  AllPlayersData  AllPlayersData { get; protected set; }
    }
}