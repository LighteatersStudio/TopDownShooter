using UnityEngine;

namespace Services.AppVersion.Coloring
{
    [RequireComponent(typeof(Renderer))]
    public class MaterialColoring : ColoringBase
    {
        private Renderer _renderer;
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }
        
        protected override void ChangeColor(IColorSchema schema)
        {
            _renderer.material.color = schema.Second;
        }
    }
}