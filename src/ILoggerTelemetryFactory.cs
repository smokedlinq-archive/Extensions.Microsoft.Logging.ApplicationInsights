namespace Microsoft.Extensions.DependencyInjection
{
    public interface ILoggerTelemetryFactory
    {
        ILoggerTelemetry CreateLogger<T>();
        ILoggerTelemetry CreateLogger(string categoryName);
    }
}