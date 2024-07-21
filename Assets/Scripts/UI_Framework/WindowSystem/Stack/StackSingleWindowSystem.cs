using System.Collections.Generic;
using UnityEngine;

namespace UI.Framework.Implementation
{
    internal class StackSingleWindowSystem : CanvasWindowSystemBase
    {
        private readonly Stack<StackItem> _stack = new();
        
        [SerializeField]private StackItem _stackItemPrefab;
        
        protected override TView Open<TView>(UIBuilder builder, GameObject prefab)
        {
            var stackItem = CreateStackView();
            var view = builder.Build<TView>(prefab, stackItem.transform);
            stackItem.Construct(view);
            
            if(_stack.TryPeek(out var current))
            {
                current.Closed -= OnItemClosed;
                current.Hide();
            }

            _stack.Push(stackItem);
            OpenCurrent();
            
            return view;
        }
        
        private StackItem CreateStackView()
        {
            var result = Instantiate(_stackItemPrefab, transform);
            result.Show();
            
            return result;
        }

        private void OpenCurrent()
        {
            StackItem current;
            
            while (_stack.TryPeek(out current))
            {
                if(current != null && !current.IsEmpty)
                {
                    break;
                }
                
                _stack.Pop();
            }
            
            if (current == null)
            {
                return;
            }
            
            current.Closed += OnItemClosed;
            current.Show();
        }

        private void OnItemClosed(StackItem item)
        {
            item.Closed -= OnItemClosed;
            Destroy(item);
            _stack.Pop();
            
            OpenCurrent();
        }
    }
}