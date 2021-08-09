using System.Collections.Generic;
using NSubstitute;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Interfaces;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.TestingCore.Metrics
{
  public class MetricServiceCallValidator
  {
    private readonly Dictionary<string, string> _tags;
    private readonly Dictionary<string, object> _fields;

    public MetricServiceCallValidator()
    {
      _tags = new Dictionary<string, string>();
      _fields = new Dictionary<string, object>();
    }


    // Builder methods
    public MetricServiceCallValidator ContainsTag(string tag, string expected)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.ContainsTag) Add tests
      _tags[tag] = expected;
      return this;
    }

    public MetricServiceCallValidator WithCustomTag1(string expected)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.WithCustomTag1) Add tests
      _tags[MetricTag.Tag1] = expected;
      return this;
    }

    public MetricServiceCallValidator WithCustomTag2(string expected)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.WithCustomTag2) Add tests
      _tags[MetricTag.Tag2] = expected;
      return this;
    }

    public MetricServiceCallValidator WithCustomTag3(string expected)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.WithCustomTag3) Add tests
      _tags[MetricTag.Tag3] = expected;
      return this;
    }

    public MetricServiceCallValidator WithCustomTag4(string expected)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.WithCustomTag4) Add tests
      _tags[MetricTag.Tag4] = expected;
      return this;
    }

    public MetricServiceCallValidator WithField(string field, long expected)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.WithField) Add tests
      _fields[field] = expected;
      return this;
    }

    public MetricServiceCallValidator WithField(string field, double expected)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.WithField) Add tests
      _fields[field] = expected;
      return this;
    }

    public MetricServiceCallValidator WithField(string field, float expected)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.WithField) Add tests
      _fields[field] = expected;
      return this;
    }

    public MetricServiceCallValidator WithField(string field, int expected)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.WithField) Add tests
      _fields[field] = expected;
      return this;
    }


    // Finalization methods
    public void CalledForSubmitBuilderAsync(IMetricService metricService)
    {
      metricService.Received(1).SubmitBuilderAsync(
        Arg.Is<IMetricBuilder>(b => PassesValidation(b))
      );
    }

    public void CalledForSubmitBuilder(IMetricService metricService)
    {
      metricService.Received(1).SubmitBuilder(
        Arg.Is<IMetricBuilder>(b => PassesValidation(b))
      );
    }


    // Internal methods
    private bool PassesTagConstraints(CoreMetric metric)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.PassesTagConstraints) Add tests
      if (metric == null)
        return false;

      foreach (var (key, value) in _tags)
      {
        if (!metric.Tags.ContainsKey(key))
          return false;

        if (metric.Tags[key] != value)
          return false;
      }

      return true;
    }

    private bool PassesFieldsConstraints(CoreMetric metric)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.PassesFieldsConstraints) Add tests
      if (metric == null)
        return false;

      foreach (var (key, value) in _fields)
      {
        if (!metric.Fields.ContainsKey(key))
          return false;

        var currentField = metric.Fields[key];

        if (currentField.GetType() != value.GetType())
          return false;

        if (currentField != value)
          return false;
      }

      return true;
    }

    private bool PassesValidation(IMetricBuilder builder)
    {
      // TODO: [TESTS] (MetricServiceCallValidator.PassesValidation) Add tests
      var metric = builder.Build();

      return PassesTagConstraints(metric) &&
             PassesFieldsConstraints(metric);
    }
  }
}
