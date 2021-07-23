using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Rn.NetCore.TestingCore.Builders
{
  public class InMemoryConfigurationBuilder
  {
    public string Key { get; private set; }
    public string AppendKey { get; private set; }
    private readonly Dictionary<string, string> _config;

    public InMemoryConfigurationBuilder()
    {
      // TODO: [TESTS] (InMemoryConfigurationBuilder) Add tests
      _config = new Dictionary<string, string>();
      Key = string.Empty;
      AppendKey = string.Empty;
    }

    public InMemoryConfigurationBuilder(string key)
      : this()
    {
      // TODO: [TESTS] (InMemoryConfigurationBuilder) Add tests
      Key = key;
      AppendKey = $"{key}:";
    }


    // Builder methods
    public InMemoryConfigurationBuilder WithSection(string key, Func<InMemoryConfigurationBuilder, InMemoryConfigurationBuilder> func)
    {
      // TODO: [TESTS] (InMemoryConfigurationBuilder.WithSection) Add tests
      var configuration = func
        ?.Invoke(new InMemoryConfigurationBuilder($"{AppendKey}{key}"))
        ?.GetDictionary();

      foreach (var configKey in configuration.Keys)
      {
        _config[configKey] = configuration[configKey];
      }

      return this;
    }

    public InMemoryConfigurationBuilder WithKey(string key, string value)
    {
      // TODO: [TESTS] (InMemoryConfigurationBuilder.WithKey) Add tests
      _config[$"{AppendKey}{key}"] = value;

      return this;
    }

    public InMemoryConfigurationBuilder WithKey(string key, int value)
    {
      // TODO: [TESTS] (InMemoryConfigurationBuilder.WithKey) Add tests
      _config[$"{AppendKey}{key}"] = value.ToString("D");

      return this;
    }

    public InMemoryConfigurationBuilder WithKey(string key, bool value)
    {
      // TODO: [TESTS] (InMemoryConfigurationBuilder.WithKey) Add tests
      _config[$"{AppendKey}{key}"] = value ? "true" : "false";

      return this;
    }


    // Finalization methods
    public Dictionary<string, string> GetDictionary()
    {
      // TODO: [TESTS] (InMemoryConfigurationBuilder.GetDictionary) Add tests
      return _config;
    }

    public IConfiguration Build()
    {
      return new ConfigurationBuilder()
        .AddInMemoryCollection(_config)
        .Build();
    }
  }
}
