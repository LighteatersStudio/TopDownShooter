using System;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class TaskProcessQueue : ITaskProcessQueue
    {
        private Task _currentTask = Task.CompletedTask;

        public Task Start(Func<Task> actionLauncher)
        {
            if (_currentTask.IsCompleted)
            {
                _currentTask = Task.CompletedTask;
            }

            var action = new TaskAwaiter(_currentTask, actionLauncher);
            _currentTask = action.Await();

            return _currentTask;
        }
    }
}