using System;
using UnityEngine;

namespace Coloring
{
    [CreateAssetMenu(menuName = "LightEaters/Coloring/Create ColorSchema", fileName = "ColorSchema", order = 0)]
    public class ColorSchema : ScriptableObject, IColorSchema
    {
        [SerializeField] private Color main = Color.white;
        [SerializeField] private Color second = Color.white;
        [SerializeField] private Color third = Color.white;
        
        public Color Main => main;
        public Color Second => second;
        public Color Third => third;
    }
}