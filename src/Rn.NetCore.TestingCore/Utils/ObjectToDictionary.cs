using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Rn.NetCore.Common.Helpers;

namespace Rn.NetCore.TestingCore.Utils
{
  public class ObjectToDictionary
  {
    private readonly IJsonHelper _jsonHelper;

    public ObjectToDictionary(IJsonHelper jsonHelper)
    {
      _jsonHelper = jsonHelper;
    }


    // Public methods
    public Dictionary<string, string> Extract(object config, string key)
    {
      var dictionary = new Dictionary<string, string>();
      if (string.IsNullOrWhiteSpace(key)) key = "";
      var appendKey = key + (key.Length > 0 ? ":" : "");

      var jObject = _jsonHelper.DeserializeObject<JObject>(
        _jsonHelper.SerializeObject(config)
      );

      RecurseAppendKey(appendKey, dictionary, jObject);

      return dictionary;
    }


    // Internal methods
    private static void RecurseAppendKey(string key, IDictionary<string, string> dictionary, JObject obj)
    {
      if (obj == null) return;

      foreach (var (objKey, objValue) in obj)
      {
        var currentKey = $"{key}{objKey}";

        if (objValue == null)
        {
          dictionary[currentKey] = string.Empty;
          continue;
        }

        switch (objValue.Type)
        {
          case JTokenType.String:
            dictionary[currentKey] = objValue.Value<string>();
            break;

          case JTokenType.Date:
            dictionary[currentKey] = objValue.Value<DateTime>().ToString("u");
            break;

          case JTokenType.Integer:
            HandleNumber(dictionary, currentKey, objValue);
            break;

          case JTokenType.Float:
            HandleFloat(dictionary, currentKey, objValue);
            break;

          case JTokenType.Boolean:
            HandleBool(dictionary, currentKey, objValue);
            break;

          case JTokenType.Object:
            RecurseAppendKey(RecurseKey(currentKey), dictionary, objValue.Value<JObject>());
            break;

          //case JTokenType.None:
          //case JTokenType.Array:
          //case JTokenType.Constructor:
          //case JTokenType.Property:
          //case JTokenType.Comment:
          //case JTokenType.Null:
          //case JTokenType.Undefined:
          //case JTokenType.Raw:
          //case JTokenType.Bytes:
          //case JTokenType.Guid:
          //case JTokenType.Uri:
          //case JTokenType.TimeSpan:
          default:
            throw new Exception($"{objValue.Type} is not supported");
        }
      }
    }

    private static string RecurseKey(string current)
    {
      // TODO: [TESTS] (ObjectToDictionary.RecurseKey) Add tests
      if (current.Length == 0)
        return "";

      return current + ":";
    }

    private static void HandleBool(IDictionary<string, string> dictionary, string key, JToken value)
    {
      // TODO: [TESTS] (ObjectToDictionary.HandleBool) Add tests
      dictionary[key] = value.Value<bool>() ? "true" : "false";
    }

    private static void HandleFloat(IDictionary<string, string> dictionary, string key, JToken value)
    {
      // TODO: [TESTS] (ObjectToDictionary.HandleFloat) Add tests
      dictionary[key] = value.ToString();
    }

    private static void HandleNumber(IDictionary<string, string> dictionary, string key, JToken value)
    {
      // TODO: [TESTS] (ObjectToDictionary.HandleNumber) Add tests
      dictionary[key] = value.ToString();
    }
  }
}