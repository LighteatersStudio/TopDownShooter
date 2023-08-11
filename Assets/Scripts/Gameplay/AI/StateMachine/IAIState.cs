using System.Threading.Tasks;

namespace Gameplay.AI
{
    public interface IAIState
    {
        Task<StateResult> Launch();
    }
}