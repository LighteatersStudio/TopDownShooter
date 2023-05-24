using System;
using UnityEngine;

namespace UI.Framework
{
    public abstract class View : MonoBehaviour, IView
    {
        public event Action<IView> Closed;

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            Closed?.Invoke(this);
        }
    }
}