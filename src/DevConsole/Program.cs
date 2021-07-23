using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.TestingCore.Builders;

namespace DevConsole
{
  class Program
  {
    private static IServiceProvider _serviceProvider;

    static void Main(string[] args)
    {
      ConfigureDI();

      var configuration = new InMemoryConfigurationBuilder()
        .WithSection("Test", test => test
          .WithKey("Hello", "World")
          .WithKey("Number", 1)
          .WithSection("SubTest", subTest => subTest
            .WithKey("Hello", "Again")
          )
          .WithKey("Here", "124")
        )
        .WithKey("Boolean", true)
        .Build();

      Console.WriteLine(configuration);
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
