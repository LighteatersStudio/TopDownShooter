using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Loading
{
    public static class LoadingServiceExtension
    {
        public static async Task Load(this ILoadingService service, ILoadingOperation operation)
        {
            var queue = new Queue<ILoadingOperation>();
            queue.Enqueue(operation);
            await service.Load(queue);
        }
    }
}