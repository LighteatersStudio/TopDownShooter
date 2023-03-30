using System;
using System.Threading.Tasks;

namespace Loading
{
    public interface ILoadingOperation
    {
        string Description { get; }
        
        UniTask Launch(Action<float> onProgress);
    }
}