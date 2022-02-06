using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTest.Contracts;
using WeatherTest.Integration;
using WeatherTest.Models;
using WeatherTest.Service;
using Xunit;
using static WeatherTest.Integration.JsonResultObject;

namespace WeatherTest.UnitTests.Service
{
    public class WeatherServiceTests
    {
        private JsonResultObject jsonResultObject;
        private Coords coords1;
        private Coords coords2;
        public WeatherServiceTests()
        {
            jsonResultObject = new JsonResultObject()
            {
                properties = new Properties()
                {
                    generatedAt = DateTime.Now,
                    periods = new List<Period>
                    {
                        new Period()
                        {
                            endTime = DateTime.Now.AddHours(3),
                            startTime = DateTime.Now.AddHours(2),
                            temperature = 5,
                            temperatureUnit = "F"
                        },
                        new Period()
                        {
                            endTime = DateTime.Now.AddDays(1).AddHours(4),
                            startTime = DateTime.Now.AddDays(1).AddHours(3),
                            temperature = 3,
                            temperatureUnit = "F"
                        },
                        new Period()
                        {
                            endTime = DateTime.Now.AddDays(1).AddHours(2),
                            startTime = DateTime.Now.AddDays(1).AddHours(1),
                            temperature = 5,
                            temperatureUnit = "F"
                        }
                    }
                }
            };

            coords1 = new Coords()
            {
                latitude = 32,
                longitude = 32
            };

            coords2 = new Coords()
            {
                longitude = 30,
                latitude = 30
            };
        }
        [Fact]
        public async void FilterOutWrongDays()
        {
            //Arrange
            var integration = Substitute.For<IWeatherIntegration>();

            integration.GetMedianValuesAsync(Arg.Any<Coords>()).Returns(jsonResultObject);

            var sut = new WeatherService(integration);

            //Act

            var result = await sut.GetMedianValues(new WeatherRequest
            {
               positionList = new List<Coords>
               {
                   coords1
               }
            });

            //Assert
            Assert.Equal(4,result.MedianValues.ElementAt(0));
        }
        [Fact]
        public async void Recieves_Correct_Amount_Of_Calls()
        {
            //Arrange
            var integration = Substitute.For<IWeatherIntegration>();

            integration.GetMedianValuesAsync(Arg.Any<Coords>()).Returns(jsonResultObject);

            var sut = new WeatherService(integration);

            //Act
            await sut.GetMedianValues(new WeatherRequest
            {
                positionList = new List<Coords>
               {
                   coords1,
                   coords2
               }
            });

            //Assert

            await integration.Received(1).GetMedianValuesAsync(coords1);
            await integration.Received(1).GetMedianValuesAsync(coords2);
        }
    }
}
