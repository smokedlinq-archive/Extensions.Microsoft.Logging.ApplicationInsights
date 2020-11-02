using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;

namespace Extensions.Microsoft.Logging.ApplicationInsights
{
    internal class LoggerTelemetryFactory : ILoggerTelemetryFactory
    {
        private readonly ILoggerFactory factory;
        private readonly TelemetryClient client;

        public LoggerTelemetryFactory(ILoggerFactory factory, TelemetryClient client)
        {
            this.factory = factory ?? throw new System.ArgumentNullException(nameof(factory));
            this.client = client ?? throw new System.ArgumentNullException(nameof(client));
        }

        public ILoggerTelemetry CreateLogger(string categoryName)
            => new LoggerTelemetry(factory.CreateLogger(categoryName), client);

        public ILoggerTelemetry CreateLogger<T>()
            => new LoggerTelemetry(factory.CreateLogger<T>(), client);
    }
}