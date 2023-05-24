using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.AppVersion.Loading
{
    public interface ILoadingScreen
    {
        Task Show(Queue<ILoadingOperation> queue);
    }
}