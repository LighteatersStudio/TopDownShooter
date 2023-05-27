using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "LightEaters/Player/Create PlayerSettings", fileName = "PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject, IPlayerSettings 
    {
        [SerializeField] private StatsInfo _stats;
        [SerializeField] private GameObject _model;
        
        public StatsInfo Stats => _stats;
        public GameObject Model => _model;
    }
}