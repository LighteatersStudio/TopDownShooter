using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Loading
{
    public interface ILoadingService
    {
        Task Load(Queue<ILoadingOperation> loadingOperations);
    }
}