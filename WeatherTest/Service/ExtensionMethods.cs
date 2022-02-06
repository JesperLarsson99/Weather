using System;
using System.Collections.Generic;
using System.Linq;
using WeatherTest.Integration;

namespace WeatherTest.Service
{
    public static class ExtensionMethods
    {
        public static IEnumerable<JsonResultObject.Period> FilterOutWrongDays(this JsonResultObject resultObject)
        {
            var tomorrow = DateTime.Today.AddDays(1);

            var valuesTomorrow = resultObject.properties.periods.Where(x => x.startTime.Date == tomorrow);

            return valuesTomorrow;
        }

        public static List<int> GetSortedTemperatures(this IEnumerable<JsonResultObject.Period> periods)
        {
            var temperatures = periods.Select(x => x.temperature).ToList();

            temperatures.Sort();

            return temperatures;
        }

        public static float GetMedian(this List<int> ints)
        {
            var size = ints.Count;

            var mid = size / 2;
            var median = (size % 2 != 0) ? ints[mid] : (float)(ints[mid] + (float)ints[mid - 1]) / 2;

            return median;
        }
    }
}