namespace Services
{
    public interface IApplicationVersion
    {
        string Version { get; }
        string BuildNumber { get; }
    }
}