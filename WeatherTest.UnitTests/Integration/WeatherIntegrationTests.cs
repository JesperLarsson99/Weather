using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeatherTest.Integration;
using Xunit;
using static WeatherTest.Integration.JsonResultObject;

namespace WeatherTest.UnitTests.Integration
{
    public class WeatherIntegrationTests
    {
        [Fact]
        public void GetMedianValues()
        {
            //Arrange
            var jsonResultObject = new JsonResultObject()
            {
                properties = new Properties()
                {
                    generatedAt = DateTime.Now,
                    periods = new List<Period>
                    {
                        new Period()
                        {
                            endTime = DateTime.Now.AddHours(1),
                            startTime = DateTime.Now,
                            temperature = 5,
                            temperatureUnit = "F"
                        },
                        new Period()
                        {
                            endTime = DateTime.Now.AddDays(1),
                            startTime = DateTime.Now.AddDays(1).AddHours(-1),
                            temperature = 3,
                            temperatureUnit = "F"
                        },
                        new Period()
                        {
                            endTime = DateTime.Now.AddDays(1).AddHours(-1),
                            startTime = DateTime.Now.AddDays(1).AddHours(-2),
                            temperature = 5,
                            temperatureUnit = "F"
                        }
                    }
                }
            };

            var jsonSerializedObject = JsonConvert.SerializeObject(jsonResultObject);
            var iHttpClientFactory = Substitute.For<IHttpClientFactory>();
            var httpMessageHandler = new MockHttpMessageHandler(jsonSerializedObject);
            var httpClient = new HttpClient(httpMessageHandler, false);
            httpClient.BaseAddress = new Uri("https://localhost:3647");

            iHttpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

            var coords = new Models.Coords { longitude = 32, latitude = 32 };

            WeatherIntegration weatherIntegration = new WeatherIntegration(iHttpClientFactory);

            //Act
            var result = weatherIntegration.GetMedianValuesAsync(coords);

            //Assert
            Assert.NotNull(result.Result.properties);
            Assert.Equal("https://localhost:3647/gridpoints/TOP/32,32/forecast/hourly", httpMessageHandler.RequestUri);
            Assert.Equal(result.Result.properties.generatedAt, jsonResultObject.properties.generatedAt);
        }
        public class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly string content;
            public string RequestUri;

            public MockHttpMessageHandler(string content)
            {
                this.content = content;
            }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                RequestUri = request.RequestUri.ToString();
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(content)
                };
            }
        }
    }
}