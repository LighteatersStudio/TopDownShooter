using System;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class TaskProcessQueueThreadSave : TaskProcessQueue
    {
        public override Task Start(Func<Task> actionLauncher)
        {
            Task task;

            lock (this)
            {
                task = base.Start(actionLauncher);
            }

            return task;
        }
    }
}