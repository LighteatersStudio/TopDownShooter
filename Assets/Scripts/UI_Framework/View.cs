using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UI.Framework
{
    public abstract class View : MonoBehaviour, IView
    {
        private TaskCompletionSource<bool> _closeAwaiter;

        public event Action<IView> Closed;

        public Task CloseAwaiter => _closeAwaiter.Task;

        public bool IsAlive => this;

        public virtual void Open()
        {
            _closeAwaiter?.TrySetResult(true);
            _closeAwaiter = new TaskCompletionSource<bool>();
                
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
            _closeAwaiter?.TrySetResult(true);
            
            Closed?.Invoke(this);

            Destroy(gameObject);
        }
    }
}