using System;
using System.Threading.Tasks;

namespace UI.Framework
{
    public interface IView
    {
        event Action<IView> Closed;

        bool IsAlive { get; }

        Task CloseAwaiter { get; }

        void Open();
        void Close();
    }
}