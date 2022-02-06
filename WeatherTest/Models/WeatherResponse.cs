using System.Collections.Generic;
using WeatherTest.Contracts;

namespace WeatherTest.Models
{
    public class WeatherResponse : IWeatherResponse
    {
        public IEnumerable<float> MedianValues { get; set; }
    }
}
