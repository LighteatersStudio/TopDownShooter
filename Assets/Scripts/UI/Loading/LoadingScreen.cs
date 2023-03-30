using System;
using System.Threading.Tasks;
using Loading;
using UnityEngine;

namespace UI
{
    public class LoadingScreen : MonoBehaviour, IView
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

        public async void Load(ILoadingOperation loadingOperation, bool closeAfterLoad = true)
        {
            await Task.Delay(2000);
            
            await loadingOperation.Launch();

            if (closeAfterLoad)
            {
                Close();
            }
        }
    }
}