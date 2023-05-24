using System;

namespace UI.Framework
{
    public interface IView
    {
        event Action<IView> Closed;
        
        void Open();
        void Close();
    }
}