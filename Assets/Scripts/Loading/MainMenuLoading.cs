using System.Threading.Tasks;
using Loader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loading
{
    public class MainMenuLoading : ILoadingOperation
    {
        public Task Launch()
        {
            TaskCompletionSource<bool> loadingTask = new();

            var task = SceneManager.LoadSceneAsync(SceneNames.MainMenu);

            task.completed += LoadedHandler;

            return loadingTask.Task;
            
            void LoadedHandler(AsyncOperation operation)
            {
                loadingTask.SetResult(true);   
            }
        }
    }
}