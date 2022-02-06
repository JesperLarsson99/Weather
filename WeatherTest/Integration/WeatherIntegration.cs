using Newtonsoft.Json;
using Polly;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherTest.Contracts;
using WeatherTest.Models;

namespace WeatherTest.Integration
{
    public class WeatherIntegration : IWeatherIntegration
    {
        private readonly HttpClient client;
        HttpStatusCode[] httpStatusCodesWorthRetrying = {
        HttpStatusCode.RequestTimeout, // 408
        HttpStatusCode.InternalServerError, // 500
        HttpStatusCode.BadGateway, // 502
        HttpStatusCode.ServiceUnavailable, // 503
        HttpStatusCode.GatewayTimeout // 504
        };

        public WeatherIntegration(IHttpClientFactory client)
        {
            this.client = client.CreateClient("httpClient");
        }

        public async Task<JsonResultObject> GetMedianValuesAsync(Coords coords)
        {
            HttpResponseMessage result = await Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => httpStatusCodesWorthRetrying.Contains(r.StatusCode))
            .RetryAsync(5)
            .ExecuteAsync(async () =>
            {
                try
                {
                    var response = await client.GetAsync($"gridpoints/TOP/{coords.latitude},{coords.longitude}/forecast/hourly");
                    return response;
                }
                catch (Exception e)
                {
                    return null;
                }
            });

            var resultString = result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<JsonResultObject>(resultString);
        }
    }
}