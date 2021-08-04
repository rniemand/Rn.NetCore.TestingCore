using System;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Common.Metrics.Interfaces;

namespace Rn.NetCore.TestingCore.Builders
{
  public class ServiceProviderBuilder
  {
    private readonly IServiceProvider _provider;

    public ServiceProviderBuilder()
    {
      _provider = Substitute.For<IServiceProvider>();
    }

    public ServiceProviderBuilder WithLogger<TLogger>(ILoggerAdapter<TLogger> logger = null)
    {
      _provider
        .GetService(typeof(ILoggerAdapter<TLogger>))
        .Returns(logger ?? Substitute.For<ILoggerAdapter<TLogger>>());

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
