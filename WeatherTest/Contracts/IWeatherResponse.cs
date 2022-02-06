using System.Collections.Generic;

namespace WeatherTest.Contracts
{
    public interface IWeatherResponse
    {
        public IEnumerable<float> MedianValues { get; set; }
    }
}
