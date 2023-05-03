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
        private UIAudioBuilder _audioBuilder;
        
        
        [Inject]
        public void Construct(UIBuilder uiBuilder, UIAudioBuilder audioBuilder)
        {
            _builder = uiBuilder;
            _audioBuilder = audioBuilder;
        }
        
        public TView Open<TView>() where TView : IView
        {
            var newView = _builder.Build<TView>(_menuRoot);

            TryFillSounds(newView);
            
            if (newView == null)
            {
                Debug.LogError("View not opened!");
                return default;
            }
            
            if(_viewStack.Any())
            {
                var current = _viewStack.Peek();
                
                current.Closed -= OnViewClosed;
                current.Close();
            }
            
            _viewStack.Push(newView);
            OpenCurrent();
            return newView;
        }

        private void TryFillSounds<TView>(TView newView) where TView : IView
        {
            if (newView is View view)
            {
                _audioBuilder.FillSounds(view);
            }
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
            if(! _viewStack.Any())
            {
                return;
            }
            
            var current = _viewStack.Peek();
            current.Closed += OnViewClosed;
            current.Open();
        }
    }
}