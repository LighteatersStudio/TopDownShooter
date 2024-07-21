using UnityEngine;

namespace Infrastructure.Loading
{
    [CreateAssetMenu(fileName = "ShopSettings", menuName = "LightEaters/Loading/ShopSettings")]
    public class ShopSettings : ScriptableObject
    {
        [field: SerializeField] public string ShopSceneName { get; private set; }
    }
}