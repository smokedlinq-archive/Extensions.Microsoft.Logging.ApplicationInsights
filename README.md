# Extensions.Microsoft.Logging.ApplicationInsights

![Build](https://github.com/smokedlinq/Extensions.Microsoft.Logging.ApplicationInsights/workflows/Build/badge.svg)
[![SonarCloud Status](https://sonarcloud.io/api/project_badges/measure?project=smokedlinq_Extensions.Microsoft.Logging.ApplicationInsights&metric=alert_status)](https://sonarcloud.io/dashboard?id=smokedlinq_Extensions.Microsoft.Logging.ApplicationInsights)
[![NuGet](https://img.shields.io/nuget/dt/Extensions.Microsoft.Logging.ApplicationInsights.svg)](https://www.nuget.org/packages/Extensions.Microsoft.Logging.ApplicationInsights)
[![NuGet](https://img.shields.io/nuget/vpre/Extensions.Microsoft.Logging.ApplicationInsights.svg)](https://www.nuget.org/packages/Extensions.Microsoft.Logging.ApplicationInsights)

This package adds support for logging to both Application Insights and Microsoft.Extensions.Logging from a common interface thus removing the need for a dependency on `ILogger` and `TelemetryClient`.

## Getting started

First, configure dependency injection to include the `ILoggerTelemetry<T>` and `ILoggerTelemetryFactory` interfaces.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add logging and Application Insights if your platform does not
    // services.AddLogging();
    // serivces.AddApplicationInsightsTelemetry();
    services.AddLoggerTelemetry()
}
```

Then use one of the following to take a dependency:

```csharp
private readonly ILoggerTelemetry logger;

public MyClass(ILoggerTelemetry<MyClass> logger)
    => this.logger = logger;

public MyClass(ILoggerTelemetryFactory loggerFactory)
    => this.logger = loggerFactory.CreateLogger<MyClass>();
```

Finally, use the class to log like you would with `ILogger` and also be able to pass availability and event telemetry to the `TelemetryClient`.

```csharp
public void ChangeTheThing()
{
   logger.LogTrace("About to change the thing");
   // ...
   logger.LogEvent(nameof(ChangeTheThing));
}
```

> Note: Only requests to the ILoggerTelemetry methods `LogAvailability` and `LogEvent` are directly passed to the TelemetryClient, all other logging is passed to the `ILogger`. The ILogger needs to be configured to use the Application Insights provider to forward other telemetry.
> 
