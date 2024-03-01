using System.Threading.Tasks;

namespace Meta.Level
{
    public interface IGameRun
    {
        Task NextRandomArena();
        Task Finish();
    }
}