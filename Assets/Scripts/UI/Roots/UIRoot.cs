using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        [SerializeField] private Transform _menuRoot;
        
        private readonly Stack<IView> _viewStack = new();
        private UIBuilder _builder;
        
        
        [Inject]
        public void Construct(UIBuilder uiBuilder)
        {
            _builder = uiBuilder;
        }
        public TView Open<TView>() where TView : IView
        {
            var newView = _builder.Build<TView>(_menuRoot);

            if (newView == null)
            {
                Debug.LogError("View not opened!");
                return default;
            }
            
            if (_viewStack.Any())
            {
                var current = _viewStack.Peek();
                
                current.Closed -= OnViewClosed;
                current.Close();
            }
            
            _viewStack.Push(newView);
            return newView;
        }

        private void OnViewClosed(IView view)
        {
            _builder.ToPool(view);
            
            view.Closed -= OnViewClosed;
            _viewStack.Pop();
            OpenCurrent();
        }

        private void OpenCurrent()
        {
            if(_viewStack.Any())
            {
                return;
            }
            var current = _viewStack.Peek();
            current.Closed += OnViewClosed;
            current.Open();
        }
    }
}