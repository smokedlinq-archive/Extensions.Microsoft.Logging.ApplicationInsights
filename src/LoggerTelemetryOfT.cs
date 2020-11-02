using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    internal class LoggerTelemetry<T> : LoggerTelemetry, ILoggerTelemetry<T>
    {
        public LoggerTelemetry(ILoggerFactory loggerFactory, TelemetryClient client)
            : base((loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory))).CreateLogger<T>(), client)
        {
        }
    }
}