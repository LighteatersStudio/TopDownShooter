using System;
using UnityEngine;

namespace UI.Framework.Implementation
{
    internal class StackItem : MonoBehaviour
    {
        public bool IsEmpty => _view == null;
        
        private IView _view;

        public event Action<StackItem> Closed;
        
        public void Construct(IView view)
        {
            _view = view;
            _view.Open();
            _view.Closed += OnViewClosed;
        }

        private void OnViewClosed(IView view)
        {
            _view.Closed -= OnViewClosed;
            _view = null;
            
            Closed?.Invoke(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}