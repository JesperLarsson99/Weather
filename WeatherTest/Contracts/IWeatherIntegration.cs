using System.Threading.Tasks;
using WeatherTest.Integration;
using WeatherTest.Models;

namespace WeatherTest.Contracts
{
    public interface IWeatherIntegration
    {
        Task<JsonResultObject> GetMedianValuesAsync(Coords coords);
    }
}