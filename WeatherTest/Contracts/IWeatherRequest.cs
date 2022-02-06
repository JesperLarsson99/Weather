using System.Collections.Generic;
using WeatherTest.Models;

namespace WeatherTest.Contracts
{
    public interface IWeatherRequest
    {
        List<Coords> positionList { get; set; } 
    }
}
