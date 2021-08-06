using System.Collections.Generic;
using NSubstitute;
using Rn.NetCore.Common.Metrics.Interfaces;

namespace Rn.NetCore.TestingCore.Metrics
{
  public class SubmitMetricAsyncValidator
  {
    private readonly Dictionary<string, string> _tags;

    public SubmitMetricAsyncValidator()
    {
      _tags = new Dictionary<string, string>();
    }

    public SubmitMetricAsyncValidator ContainsTag(string tag, string expected)
    {
      _tags[tag] = expected;
      return this;
    }

    private bool PassesValidation(IMetricBuilder builder)
    {
      var metric = builder.GetRawMetric();

      foreach (var (key, value) in _tags)
      {
        if (!metric.Tags.ContainsKey(key))
          return false;

        if (metric.Tags[key] != value)
          return false;
      }

      return true;
    }

    public void Run(IMetricService metricService)
    {
      metricService.Received(1).SubmitMetricAsync(
        Arg.Is<IMetricBuilder>(b => PassesValidation(b))
      );
    }
  }
}
