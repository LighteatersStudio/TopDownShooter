using System;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public interface ITaskProcessQueue
    {
        Task Start(Func<Task> actionLauncher);
    }
}