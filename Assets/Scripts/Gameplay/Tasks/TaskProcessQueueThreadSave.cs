using System;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class TaskProcessQueueThreadSave : ITaskProcessQueue
    {
        private readonly ITaskProcessQueue _taskProcessQueue;

        public TaskProcessQueueThreadSave(ITaskProcessQueue taskProcessQueue)
        {
            _taskProcessQueue = taskProcessQueue;
        }
        
        public Task Start(Func<Task> actionLauncher)
        {
            Task task;

            lock (this)
            {
                task = _taskProcessQueue.Start(actionLauncher);
            }

            return task;
        }
    }
}