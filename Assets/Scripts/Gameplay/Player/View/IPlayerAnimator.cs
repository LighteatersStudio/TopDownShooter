namespace Gameplay.View
{
    public interface IPlayerAnimator
    {
        void Initialize();
        void Dispose();
        void Update(float deltaTime);
    }
}