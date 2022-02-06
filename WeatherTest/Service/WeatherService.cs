using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherTest.Contracts;
using WeatherTest.Integration;
using WeatherTest.Models;

namespace WeatherTest.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherIntegration weatherIntegration;
        //For pullrequest
        public WeatherService(IWeatherIntegration weatherIntegration)
        {
            this.weatherIntegration = weatherIntegration;
        }
        public async Task<WeatherResponse> GetMedianValues(WeatherRequest request)
        {
            var weatherAtAllPlaces = new List<JsonResultObject>();

            foreach (var item in request.positionList)
            {
                weatherAtAllPlaces.Add(await weatherIntegration.GetMedianValuesAsync(item));
            }

            var medians = new List<float>();

            foreach (var item in weatherAtAllPlaces)
            {
                var filteredItems = item.FilterOutWrongDays();
                var soredTemperatures = filteredItems.GetSortedTemperatures();
                medians.Add(soredTemperatures.GetMedian());
            }

            var response = new WeatherResponse
            {
                MedianValues = medians
            };

            return response;
        }
    }
}
