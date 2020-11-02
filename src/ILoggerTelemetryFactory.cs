namespace Microsoft.Extensions.Logging
{
    public interface ILoggerTelemetryFactory
    {
        ILoggerTelemetry CreateLogger<T>();
        ILoggerTelemetry CreateLogger(string categoryName);
    }
}