namespace Gameplay
{
    public interface IDamageCalculator
    {
        float Calculate(IAttackInfo attackInfo, IStats stats);
    }
}