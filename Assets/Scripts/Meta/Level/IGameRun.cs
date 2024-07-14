using System.Threading.Tasks;

namespace Meta.Level
{
    public interface IGameRun
    {
        int CurrentLevel { get; }
        Task NextLevel();
        Task Finish();
    }
}