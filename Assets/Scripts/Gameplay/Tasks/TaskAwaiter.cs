using System;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class TaskAwaiter
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
}