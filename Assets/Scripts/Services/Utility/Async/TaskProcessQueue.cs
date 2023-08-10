using System;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class TaskProcessQueue
    {
        private class TaskAwaiter
        {
            private readonly Task _currentTask;
            private readonly Func<Task> _newTaskLauncher;
            
            public TaskAwaiter(Task currentTask, Func<Task> newTaskLauncher)
            {
                _currentTask = currentTask;
                _newTaskLauncher = newTaskLauncher;
            }

            public async Task Await()
            {
                await _currentTask;
                await _newTaskLauncher();
            }
        }
            
        private Task _currentTask = Task.CompletedTask;
            

        public virtual Task Start(Func<Task> actionLauncher)
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