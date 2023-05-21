using UnityEngine;
using Zenject;

namespace Coloring
{
    public abstract class ColoringBase : MonoBehaviour
    {
        private GameColoring _gameColoring;

        [Inject] 
        public void Construct(GameColoring gameColoring)
        {
            _gameColoring = gameColoring;
        }

        protected void Start()
        {
            ChangeColor(_gameColoring.Current);
        }
        
        protected  void OnEnable()
        {
            Subscribe();
        }

        protected  void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _gameColoring.Changed += OnColorSchemaChanged;
        }
        private void Unsubscribe()
        {
            _gameColoring.Changed -= OnColorSchemaChanged;
        }

        private void OnColorSchemaChanged(IColorSchema colorSchema)
        {
            ChangeColor(colorSchema);
        }

        protected abstract void ChangeColor(IColorSchema schema);
    }
}