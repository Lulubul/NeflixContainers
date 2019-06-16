using System;
using System.Collections.Generic;
using System.Linq;

namespace Recommendation.API.Shared
{
    public static class StringExtenstions
    {
        public static Dictionary<string, int> ConvertToDictionary(this string dbValue)
        {
            var entries = dbValue.Split(",");
            Dictionary<string, int> preference = new Dictionary<string, int>();
            foreach (var entry in entries)
            {
                var keyValue = entry.Split(":");
                preference.Add(keyValue[0], int.Parse(keyValue[1]));
            }
            return preference;
        }

        public static string ConvertToString(this Dictionary<string, int> keyValues)
        {
            return string.Join(",", keyValues.Select(entry => entry.Key + ":" + entry.Value));
        }
    }
}
