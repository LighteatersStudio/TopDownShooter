using System;

namespace UI
{
    public interface IView
    {
        event Action<IView> Closed;
        
        void Open();
        void Close();
    }
}