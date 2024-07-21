using UnityEngine;
using Zenject;

namespace UI.Framework.Implementation
{
    internal class WorldNodeUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private bool _isBillboard;
        [SerializeField] private bool _applyCamerarRotation;
        
        private IWorldUICamera _uiCamera;
        private IViewTarget _target;
        
        private IView _view;
        private Transform _transform;

        public Transform Root => _canvas.transform;

    
        [Inject]
        public void Construct(IWorldCameraProvider cameraProvider, IViewTarget target)
        {
            _uiCamera = cameraProvider.Camera;
            _target = target;
        }

        public void SetupView(IView view, int order)
        {
            _view = view;
            _view.Closed += OnClosed;
            _canvas.sortingOrder = order;
        }

        private void Start()
        {
            _transform = transform;
            if (_applyCamerarRotation)
            {
                _transform.rotation = _uiCamera.Transform.rotation;    
            }
        }

        public void Open()
        {
            gameObject.SetActive(true);
            _view.Open();
        }
        
        private void Update()
        {
            if(_isBillboard)
            {
                _transform.rotation = _uiCamera.Transform.rotation;
            }

            _transform.position = _target.Position;
        }
        
        private void OnClosed(IView obj)
        {
            if (!gameObject)
            {
                return;
            }
            
            _view.Closed -= OnClosed;
            Destroy(gameObject);
        }
        
        public class Factory : PlaceholderFactory<WorldNodeUI>
        {
        }
    }
}