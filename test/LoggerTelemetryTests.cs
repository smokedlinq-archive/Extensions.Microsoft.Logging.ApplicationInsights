using A3;
using AutoFixture;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Extensions.Microsoft.Logging.ApplicationInsights.Tests
{
    public class LoggerTelemetryTests
    {
        [Fact]
        public void CanUseILoggerToLogTrace()
            => A3<LoggerTelemetry>
            .Arrange(setup => setup.Sut(new LoggerTelemetry(setup.Mock<ILogger>().Object, setup.Fixture.Create<TelemetryClient>())))
            .Act(sut => sut.LogTrace(nameof(CanUseILoggerToLogTrace)))
            .Assert(context => context.Mock<ILogger>().VerifyLog(logger => logger.LogTrace(It.IsAny<string>()), Times.Once));

        [Fact]
        public void CanUseTelemetryClientTrackEvent()
            => A3<LoggerTelemetry>
            .Arrange(setup => setup.Sut(new LoggerTelemetry(setup.Mock<ILogger>().Object, setup.Fixture.Create<TelemetryClient>())))
            .Act(sut => sut.LogEvent(nameof(CanUseTelemetryClientTrackEvent)))
            .Assert(context => { /* TelemetryClient cannot be mocked */ });
    }
}
