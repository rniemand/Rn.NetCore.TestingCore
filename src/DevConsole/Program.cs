using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Rn.NetCore.Common.Logging;

namespace DevConsole
{
  class Program
  {
    private static IServiceProvider _serviceProvider;

    static void Main(string[] args)
    {
      ConfigureDI();

      var logger = _serviceProvider.GetRequiredService<ILoggerAdapter<Program>>();
      logger.Info("Hello World");
    }


    // DI related methods
    private static void ConfigureDI()
    {
      var services = new ServiceCollection();

      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

      ConfigureDIServices(services, config);

      _serviceProvider = services.BuildServiceProvider();
    }

    private static void ConfigureDIServices(IServiceCollection services, IConfiguration config)
    {
      services
        // Configuration
        .AddSingleton(config)

        // Logging
        .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
        .AddLogging(loggingBuilder =>
        {
          // configure Logging with NLog
          loggingBuilder.ClearProviders();
          loggingBuilder.SetMinimumLevel(LogLevel.Trace);
          loggingBuilder.AddNLog(config);
        });
    }
  }
}
