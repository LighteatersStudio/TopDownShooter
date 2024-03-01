using System.Threading.Tasks;

namespace Infrastructure.Loading
{
    public interface ILoadArenaService
    {
        Task<bool> TryLoadArena(string name);
        Task<bool> LoadRandomArena();
    }
}