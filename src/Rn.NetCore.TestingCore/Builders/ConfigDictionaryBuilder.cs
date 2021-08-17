using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Rn.NetCore.TestingCore.Builders
{
  public class ConfigDictionaryBuilder
  {
    private readonly Dictionary<string, string> _dictionary;
    private readonly string _baseKey;

    public ConfigDictionaryBuilder(string baseKey = null)
    {
      _dictionary = new Dictionary<string, string>();
      _baseKey = string.IsNullOrWhiteSpace(baseKey) ? string.Empty : $"{baseKey}:";
    }

    public ConfigDictionaryBuilder WithValue(string key, bool value)
      => WithValue(key, value ? "true" : "false");

    public ConfigDictionaryBuilder WithValue(string key, string value)
    {
      _dictionary[$"{_baseKey}{key}"] = value;
      return this;
    }

    public Dictionary<string, string> Build() => _dictionary;

    public IConfiguration BuildAsConfiguration() =>
      new ConfigurationBuilder()
        .AddInMemoryCollection(_dictionary)
        .Build();
  }
}
