using A3;
using AutoFixture;
using FluentAssertions;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Extensions.Microsoft.Logging.ApplicationInsights.Tests
{
    public class LoggerTelemetrySerivceCollectionExtensionsTests
    {
        [Fact]
        public void CanResolveILoggerTelemetryOfTFromServiceProvider()
            => A3<IServiceProvider>
            .Arrange(setup => setup.Sut(setup.Fixture.Create<IServiceProvider>()))
            .Act(sut => sut.GetService<ILoggerTelemetry<LoggerTelemetrySerivceCollectionExtensionsTests>>())
            .Assert(result => result.Should().NotBeNull());

        [Fact]
        public void CanResolveILoggerTelemetryFactoryFromServiceProvider()
            => A3<IServiceProvider>
            .Arrange(setup => setup.Sut(setup.Fixture.Create<IServiceProvider>()))
            .Act(sut => sut.GetService<ILoggerTelemetryFactory>())
            .Assert(result => result.Should().NotBeNull());

        [Fact]
        public void CanResolveILoggerTelemetryFromLoggerTelemetryFactory()
            => A3<ILoggerTelemetryFactory>
            .Arrange(setup => setup.Sut(setup.Fixture.Create<IServiceProvider>().GetRequiredService<ILoggerTelemetryFactory>()))
            .Act(sut => sut.CreateLogger<LoggerTelemetrySerivceCollectionExtensionsTests>())
            .Assert(result => result.Should().NotBeNull());

        public class ServiceProviderFixture : ICustomizeFixture<IServiceProvider>
        {
            public IServiceProvider Customize(IFixture fixture)
            {
                var services = new ServiceCollection();
                var configuration = TelemetryConfiguration.CreateDefault();

                configuration.DisableTelemetry = true;

                services.AddLogging();
                services.AddSingleton(configuration);
                services.AddTransient<TelemetryClient>();
                services.AddLoggerTelemetry();

                return services.BuildServiceProvider();
            }
        }
    }
}
