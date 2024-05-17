using UnityEngine;
using Zenject;

namespace UI.Framework.Implementation
{
    public class SimpleWindowsSystem : WindowSystemBase
    {
        [SerializeField] private Canvas _canvas;
        private Camera _camera;

        [Inject]
        public void Construct(IWorldCameraProvider worldCameraProvider)
        {
            if (worldCameraProvider.HasCamera)
            {
                _camera = worldCameraProvider.Camera.Source;
            }
        }

        private void Start()
        {
            if (_camera != null)
            {
                _canvas.worldCamera = _camera;
            }
        }

        public override void SetOrder(int order)
        {
        }

        protected override TView Open<TView>(UIBuilder builder, GameObject prefab)
        {
            var view = builder.Build<TView>(prefab, transform);

            view.Closed += OnViewClosed;
            view.Open();

            return view;
        }

        protected virtual void OnViewClosed(IView view)
        {
            view.Closed -= OnViewClosed;
        }
    }
}
