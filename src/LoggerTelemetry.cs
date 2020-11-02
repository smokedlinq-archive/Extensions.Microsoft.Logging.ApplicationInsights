using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Extensions.Microsoft.Logging.ApplicationInsights
{
    internal class LoggerTelemetry : ILoggerTelemetry
    {
        private readonly ILogger logger;
        private readonly TelemetryClient client;

        public LoggerTelemetry(ILogger logger, TelemetryClient client)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public IDisposable BeginScope<TState>(TState state)
            => logger.BeginScope(state);

        public bool IsEnabled(LogLevel logLevel)
            => logger.IsEnabled(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => logger.Log(logLevel, eventId, state, exception, formatter);

        public void LogEvent(EventTelemetry telemetry)
            => client.TrackEvent(telemetry);

        public void LogEvent(string eventName, IDictionary<string, string>? properties = null, IDictionary<string, double>? metrics = null)
            => client.TrackEvent(eventName, properties, metrics);

        public void LogAvailability(AvailabilityTelemetry telemetry)
            => client.TrackAvailability(telemetry);

        public void LogAvailability(
            string name,
            DateTimeOffset timeStamp,
            TimeSpan duration,
            string runLocation,
            bool success,
            string? message = null,
            IDictionary<string, string>? properties = null,
            IDictionary<string, double>? metrics = null)
            => client.TrackAvailability(name, timeStamp, duration, runLocation, success, message, properties, metrics);
    }
}