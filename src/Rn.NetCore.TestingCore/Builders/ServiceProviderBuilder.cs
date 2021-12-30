using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Metrics;

namespace Rn.NetCore.TestingCore.Builders
{
  public class ServiceProviderBuilder
  {
    private readonly IServiceProvider _provider;

    public ServiceProviderBuilder()
    {
      _provider = Substitute.For<IServiceProvider>();
    }

    public ServiceProviderBuilder WithLogger<TLogger>(ILogger<TLogger> logger = null)
    {
      _provider
        .GetService(typeof(ILoggerAdapter<TLogger>))
        .Returns(logger ?? Substitute.For<ILogger<TLogger>>());

      return this;
    }

    public ServiceProviderBuilder WithMetrics(IMetricService metrics = null)
    {
      _provider
        .GetService(typeof(IMetricService))
        .Returns(metrics ?? Substitute.For<IMetricService>());

      return this;
    }

    public ServiceProviderBuilder WithConfiguration(IConfiguration configuration = null)
    {
      _provider
        .GetService(typeof(IConfiguration))
        .Returns(configuration ?? Substitute.For<IConfiguration>());

      return this;
    }

    public ServiceProviderBuilder WithService<TService>() where TService : class
    {
      _provider
        .GetService(typeof(TService))
        .Returns(Substitute.For<TService>());

      return this;
    }

    public ServiceProviderBuilder WithService<TService>(TService service) where TService : class
    {
      _provider
        .GetService(typeof(TService))
        .Returns(service);

      return this;
    }

    public IServiceProvider Build() => _provider;
  }
}
