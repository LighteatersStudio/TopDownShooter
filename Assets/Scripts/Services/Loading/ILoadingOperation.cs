using System;
using Cysharp.Threading.Tasks;

namespace Services.Loading
{
    public interface ILoadingOperation
    {
        string Description { get; }
        
        UniTask Launch(Action<float> progressHandler);
    }
}