using UnityEngine;

namespace Services.AppVersion.Coloring
{
    [CreateAssetMenu(menuName = "LightEaters/Coloring/Create ColorSchemeSettings", fileName = "ColorSchemeSettings", order = 0)]
    public class ColorSchemeSettings : ScriptableObject
    {
        [SerializeField] private ColorSchema _default;
        [SerializeField] private ColorSchema _high;
        [SerializeField] private ColorSchema _stone;
        
        public IColorSchema Default => _default;
        
        public IColorSchema High => _high;
        public IColorSchema Stone => _stone;
    }
}