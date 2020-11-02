using AutoFixture;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace Extensions.Microsoft.Logging.ApplicationInsights.Tests
{
    public class TelemetryClientFixture : ICustomizeFixture<TelemetryClient>
    {
        public TelemetryClient Customize(IFixture fixture)
        {
            var configuration = TelemetryConfiguration.CreateDefault();
            configuration.DisableTelemetry = true;
            return new TelemetryClient(configuration);
        }
    }
}
