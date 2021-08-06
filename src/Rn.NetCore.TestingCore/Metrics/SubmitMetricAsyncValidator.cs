using System.Collections.Generic;
using NSubstitute;
using Rn.NetCore.Common.Metrics.Enums;
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

    public SubmitMetricAsyncValidator WithCustomTag1(string expected)
    {
      _tags[MetricTag.Tag1] = expected;
      return this;
    }

    public SubmitMetricAsyncValidator WithCustomTag2(string expected)
    {
      _tags[MetricTag.Tag2] = expected;
      return this;
    }

    public SubmitMetricAsyncValidator WithCustomTag3(string expected)
    {
      _tags[MetricTag.Tag3] = expected;
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
