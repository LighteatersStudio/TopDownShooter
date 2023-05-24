namespace Services.AppVersion
{
    public interface IApplicationVersion
    {
        string Version { get; }
        string BuildNumber { get; }
    }
}