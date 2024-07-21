using UnityEngine;
using Zenject;

namespace UI.Framework.Implementation
{
    internal abstract class CanvasWindowSystemBase : WindowSystemBase
    {
        [SerializeField] private Canvas _canvas;
        
        private Camera _uiCamera;

        [Inject]
        public void Construct(Camera uiCamera)
        {
            _uiCamera = uiCamera;
        }
        
        private void Start()
        {
            _canvas.worldCamera = _uiCamera;
        }
        
        public override void SetOrder(int order)
        {
            _canvas.sortingOrder = order;
        }
    }
}