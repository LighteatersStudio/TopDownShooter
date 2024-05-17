namespace UI.Framework.Implementation
{
    public interface IWorldCameraProvider
    {
        bool HasCamera{ get; }
        IWorldUICamera Camera { get; }
    }
}