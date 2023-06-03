namespace Services.Application.Version
{
    public interface IApplicationVersion
    {
        string Version { get; }
        string BuildNumber { get; }
    }
}