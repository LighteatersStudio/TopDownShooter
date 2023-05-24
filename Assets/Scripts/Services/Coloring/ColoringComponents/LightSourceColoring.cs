using UnityEngine;

namespace Services.AppVersion.Coloring
{
    [RequireComponent(typeof(Light))]
    public class LightSourceColoring : ColoringBase
    {
        private Light _light;
        private void Awake()
        {
            _light = GetComponent<Light>();
        }
        
        protected override void ChangeColor(IColorSchema schema)
        {
            _light.color = schema.Main;
        }
    }
}