using Newtonsoft.Json;
using System.Collections.Generic;
using WeatherTest.Attributes;
using WeatherTest.Contracts;

namespace WeatherTest.Models
{
    [Limit]
    public class WeatherRequest : IWeatherRequest
    {
        public List<Coords> positionList { get; set; }

        [JsonIgnore]
        public bool IsValid;
    }
}
