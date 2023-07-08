using UnityEngine;
using UnityEngine.UI;

namespace Services.Coloring
{
    [RequireComponent(typeof(Image))]
    public class ImageColoring : ColoringBase
    {
        [SerializeField] private float _alphaValue = 1;
        
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }
        
        protected override void ChangeColor(IColorSchema schema)
        {
            _image.color = new Color(schema.Main.r, schema.Main.g, schema.Main.b, _alphaValue);
        }
    }
}