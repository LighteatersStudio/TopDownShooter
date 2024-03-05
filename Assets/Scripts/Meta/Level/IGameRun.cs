using System.Threading.Tasks;

namespace Meta.Level
{
    public interface IGameRun
    {
        Task NextLevel();
        Task Finish();
    }
}