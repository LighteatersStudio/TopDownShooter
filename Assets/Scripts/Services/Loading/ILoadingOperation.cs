using System;
using System.Threading.Tasks;

namespace Services.Loading
{
    public interface ILoadingOperation
    {
        string Description { get; }
        
        Task Launch(Action<float> progressHandler);

        void AfterFinish();

        public class Fake : ILoadingOperation
        {
            public string Description => "FakeLoading";
            public Task Launch(Action<float> progressHandler)
            {
                progressHandler?.Invoke(0.5f);
                progressHandler?.Invoke(1);
                return Task.CompletedTask;
            }
            public void AfterFinish()
            {
            }
        }
    }
}