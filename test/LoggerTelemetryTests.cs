using A3;
using AutoFixture;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Extensions.Microsoft.Logging.ApplicationInsights.Tests
{
    public class LoggerTelemetryTests
    {
        [Fact]
        public void CanUseLogEvent()
            => A3<LoggerTelemetry>
            .Arrange(setup => setup.Sut(new LoggerTelemetry(setup.Mock<ILogger>().Object, setup.Fixture.Create<TelemetryClient>())))
            .Act(sut => sut.LogEvent(nameof(CanUseLogEvent)))
            .Assert(context => { /* TelemetryClient cannot be mocked */ });

        [Fact]
        public void CanUseLogAvailability()
            => A3<LoggerTelemetry>
            .Arrange<AvailabilityTelemetry>(setup =>
            {
                setup.Sut(new LoggerTelemetry(setup.Mock<ILogger>().Object, setup.Fixture.Create<TelemetryClient>()));
                return setup.Fixture.Create<AvailabilityTelemetry>();
            })
            .Act((sut, telemetry) => sut.LogAvailability(telemetry))
            .Assert(context => { /* TelemetryClient cannot be mocked */ });

        [Fact]
        public void CanUseLogTrace()
            => A3<LoggerTelemetry>
            .Arrange(setup => setup.Sut(new LoggerTelemetry(setup.Mock<ILogger>().Object, setup.Fixture.Create<TelemetryClient>())))
            .Act(sut => sut.LogTrace(nameof(CanUseLogTrace)))
            .Assert(context => context.Mock<ILogger>().VerifyLog(logger => logger.LogTrace(It.IsAny<string>()), Times.Once));

        [Fact]
        public void CanUseBeginScope()
            => A3<LoggerTelemetry>
            .Arrange(setup => setup.Sut(new LoggerTelemetry(setup.Mock<ILogger>().Object, setup.Fixture.Create<TelemetryClient>())))
            .Act(sut => { sut.BeginScope(this); })
            .Assert(context => context.Mock<ILogger>().Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Once));

        [Fact]
        public void CanUseIsEnabled()
            => A3<LoggerTelemetry>
            .Arrange(setup => setup.Sut(new LoggerTelemetry(setup.Mock<ILogger>().Object, setup.Fixture.Create<TelemetryClient>())))
            .Act(sut => { sut.IsEnabled(LogLevel.Trace); })
            .Assert(context => context.Mock<ILogger>().Verify(x => x.IsEnabled(LogLevel.Trace), Times.Once));
    }
}
