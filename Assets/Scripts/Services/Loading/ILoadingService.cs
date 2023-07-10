using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Loading
{
    public interface ILoadingService
    {
        Task Load(Queue<ILoadingOperation> loadingOperations);
        
        public class Fake : ILoadingService
        {
            public Task Load(Queue<ILoadingOperation> loadingOperations)
            {
                return Task.CompletedTask;
            }
        }
    }
}