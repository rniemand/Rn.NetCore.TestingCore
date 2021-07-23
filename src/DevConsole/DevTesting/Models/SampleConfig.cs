using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DevConsole.DevTesting.Models
{
  public class SampleConfig
  {
    [JsonProperty("Name"), JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonProperty("SampleDate"), JsonPropertyName("SampleDate")]
    public DateTime SampleDate { get; set; }

    [JsonProperty("SubConfig"), JsonPropertyName("SubConfig")]
    public SampleSubConfig SubConfig { get; set; }

    [JsonProperty("Int"), JsonPropertyName("Int")]
    public int Int { get; set; }

    [JsonProperty("Long"), JsonPropertyName("Long")]
    public long Long { get; set; }

    [JsonProperty("Double"), JsonPropertyName("Double")]
    public double Double { get; set; }

    [JsonProperty("Float"), JsonPropertyName("Float")]
    public float Float { get; set; }

    [JsonProperty("Decimal"), JsonPropertyName("Decimal")]
    public decimal Decimal { get; set; }

    public SampleConfig()
    {
      Name = string.Empty;
      SubConfig = new SampleSubConfig();
      SampleDate = DateTime.UtcNow;
      Int = int.MaxValue;
      Long = long.MaxValue;
      Double = double.MaxValue;
      Float = float.MaxValue;
      Decimal = decimal.MaxValue;
    }
  }

  public class SampleSubConfig
  {
    [JsonProperty("Name"), JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonProperty("Number"), JsonPropertyName("Number")]
    public int Number { get; set; }

    [JsonProperty("Boolean"), JsonPropertyName("Boolean")]
    public bool Boolean { get; set; }

    public SampleSubConfig()
    {
      Name = string.Empty;
      Number = 0;
      Boolean = false;
    }
  }
}
