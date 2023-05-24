using System;
using Cysharp.Threading.Tasks;

namespace Services.AppVersion.Loading
{
    public interface ILoadingOperation
    {
        string Description { get; }
        
        UniTask Launch(Action<float> progressHandler);
    }
}