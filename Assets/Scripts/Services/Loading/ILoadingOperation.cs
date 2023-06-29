using System;
using System.Threading.Tasks;

namespace Services.Loading
{
    public interface ILoadingOperation
    {
        string Description { get; }
        
        Task Launch(Action<float> progressHandler);

        void AfterFinish();
    }
}