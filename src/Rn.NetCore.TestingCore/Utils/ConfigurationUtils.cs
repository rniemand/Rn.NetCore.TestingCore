using Microsoft.Extensions.Configuration;
using Rn.NetCore.Common.Helpers;

namespace Rn.NetCore.TestingCore.Utils
{
  public class ConfigurationUtils
  {
    public static IConfiguration FromObject(object config, string key = null)
    {
      // TODO: [TESTS] (ConfigurationUtils.FromObject) Add tests
      var jsonHelper = new JsonHelper();
      var safeKey = string.IsNullOrWhiteSpace(key) ? string.Empty : key;

      return new ConfigurationBuilder()
        .AddInMemoryCollection(
          new ObjectToDictionary(jsonHelper).Extract(config, safeKey)
        )
        .Build();
    }
  }
}
