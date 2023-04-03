using System.Threading.Tasks;

namespace Level
{
    public interface IGameRun
    {
        Task Start();
        Task Finish();
    }
}