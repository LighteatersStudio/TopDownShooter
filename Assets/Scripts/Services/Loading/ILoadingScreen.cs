using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Loading
{
    public interface ILoadingScreen
    {
        Task Show(Queue<ILoadingOperation> queue);
    }
}