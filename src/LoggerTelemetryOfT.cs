using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using System;

namespace Extensions.Microsoft.Logging.ApplicationInsights
{
    internal class LoggerTelemetry<T> : LoggerTelemetry, ILoggerTelemetry<T>
    {
        public LoggerTelemetry(ILoggerFactory loggerFactory, TelemetryClient client)
            : base((loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory))).CreateLogger<T>(), client)
        {
        }
    }
}