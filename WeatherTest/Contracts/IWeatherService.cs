using System.Threading.Tasks;
using WeatherTest.Models;

namespace WeatherTest.Contracts
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetMedianValues(WeatherRequest request);
    }
}
