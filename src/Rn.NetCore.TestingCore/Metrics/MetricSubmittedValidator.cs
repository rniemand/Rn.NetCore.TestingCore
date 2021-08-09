using System.Collections.Generic;
using NSubstitute;
using Rn.NetCore.Common.Metrics.Enums;
using Rn.NetCore.Common.Metrics.Interfaces;
using Rn.NetCore.Common.Metrics.Models;

namespace Rn.NetCore.TestingCore.Metrics
{
  public class MetricSubmittedValidator
  {
    private readonly Dictionary<string, string> _tags;
    private readonly Dictionary<string, object> _fields;

    public MetricSubmittedValidator()
    {
      _tags = new Dictionary<string, string>();
      _fields = new Dictionary<string, object>();
    }


    // Builder methods
    public MetricSubmittedValidator ContainsTag(string tag, string expected)
    {
      _tags[tag] = expected;
      return this;
    }

    public MetricSubmittedValidator WithCustomTag1(string expected)
    {
      _tags[MetricTag.Tag1] = expected;
      return this;
    }

    public MetricSubmittedValidator WithCustomTag2(string expected)
    {
      _tags[MetricTag.Tag2] = expected;
      return this;
    }

    public MetricSubmittedValidator WithCustomTag3(string expected)
    {
      _tags[MetricTag.Tag3] = expected;
      return this;
    }

    public MetricSubmittedValidator WithCustomTag4(string expected)
    {
      _tags[MetricTag.Tag4] = expected;
      return this;
    }

    public MetricSubmittedValidator WithField(string field, long expected)
    {
      _fields[field] = expected;
      return this;
    }

    public MetricSubmittedValidator WithField(string field, double expected)
    {
      _fields[field] = expected;
      return this;
    }

    public MetricSubmittedValidator WithField(string field, float expected)
    {
      _fields[field] = expected;
      return this;
    }

    public MetricSubmittedValidator WithField(string field, int expected)
    {
      _fields[field] = expected;
      return this;
    }


    // Finalization methods
    public void VerifySubmitBuilderAsync(IMetricService metricService)
    {
      metricService.Received(1).SubmitBuilderAsync(
        Arg.Is<IMetricBuilder>(b => PassesValidation(b))
      );
    }

    public void VerifySubmitBuilder(IMetricService metricService)
    {
      metricService.Received(1).SubmitBuilder(
        Arg.Is<IMetricBuilder>(b => PassesValidation(b))
      );
    }


    // Internal methods
    private bool PassesTagConstraints(CoreMetric metric)
    {
      // TODO: [TESTS] (MetricSubmittedValidator.PassesTagConstraints) Add tests
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
      // TODO: [TESTS] (MetricSubmittedValidator.PassesFieldsConstraints) Add tests
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
      // TODO: [TESTS] (MetricSubmittedValidator.PassesValidation) Add tests
      var metric = builder.Build();

      return PassesTagConstraints(metric) &&
             PassesFieldsConstraints(metric);
    }
  }
}
